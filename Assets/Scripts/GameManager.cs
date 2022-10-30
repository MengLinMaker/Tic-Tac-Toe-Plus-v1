using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable] public class GameManagerEvent : UnityEvent<GameManager> { }



public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject tokenParent;
    [SerializeField] private int numConsecutiveWin;
    [SerializeField] private GameManagerEvent winEvent;
    [SerializeField] private GameManagerEvent gameOverEvent;
    private int playerTurn = 0;
    private int turns = 0;
    private int[,] winTokenPos;
    private bool hasWonGame = false;
    private GameObject[,] prefabArray;
    private bool[,,] playerTokenArray;



    [ExecuteInEditMode] private void OnValidate() {
        numConsecutiveWin = Mathf.Clamp(numConsecutiveWin, 3, 6);
    }

    IEnumerator ExecuteAfterTime(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    private void Start() {
        int height = PlayerPrefs.GetInt("height", 3);
        int width = PlayerPrefs.GetInt("width", 3);
        numConsecutiveWin = PlayerPrefs.GetInt("numConsecutive", 3);
        prefabArray = new GameObject[width, height];
        playerTokenArray = new bool[prefabs.Length, width, height];
        winTokenPos = new int[numConsecutiveWin,2];
        foreach (Transform child in tokenParent.transform)
        {   
            GameObject.Destroy(child.gameObject);
        }
    }

    public void TileClicked(Tile tile) {
        // Get tile position
        Vector3 position = tile.transform.position;

        if (NoPrefabAtPos((int) position.x, (int) position.y) == true) {
            if (hasWonGame == false) { SpawnPrefabAtPos(prefabs[playerTurn], position); }
            if (CheckWinCondition((int)position.x, (int)position.y, numConsecutiveWin) == true) {
                hasWonGame = true;
                winEvent?.Invoke(this);
                StartCoroutine(ExecuteAfterTime(1f, () => gameOverEvent?.Invoke(this) ));
            }
            CheckFullBoard();
            NextPlayerTurn();
        }
    }

    private bool NoPrefabAtPos(int posX, int posY) {
        return prefabArray[posX, posY] == null;
    }

    private void CheckFullBoard() {
        turns++;
        if (turns == playerTokenArray.GetLength(1) * playerTokenArray.GetLength(2)) {
            StartCoroutine(ExecuteAfterTime(1f, () => gameOverEvent?.Invoke(this)));
        };
    }

    private void NextPlayerTurn() {
        playerTurn++;
        playerTurn = playerTurn % prefabs.Length;
    }

    private void SpawnPrefabAtPos(GameObject prefab, Vector3 position) {
        position.z--;
        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
        gameObject.transform.parent = tokenParent.transform;

        // Save position of prefab
        prefabArray[(int)position.x, (int)position.y] = prefab;
        playerTokenArray[playerTurn, (int)position.x, (int)position.y] = true;
    }

    private bool CheckWinCondition(int posX, int posY, int numConsecutive)
    {
        // Check horizontal
        int consecutive = 0;
        int minPosX = Mathf.Max(posX - (numConsecutive - 1), 0);
        int maxPosX = Mathf.Min(posX + (numConsecutive - 1), playerTokenArray.GetLength(1)-1);
        for (int x = minPosX; x <= maxPosX; x++) {
            winTokenPos[consecutive, 0] = x;
            winTokenPos[consecutive, 1] = posY;
            consecutive = playerTokenArray[playerTurn, x, posY] ? ++consecutive : 0;
            if (consecutive == numConsecutive) {return true;}
        }

        // Check vertical
        consecutive = 0;
        int minPosY = Mathf.Max(posY - (numConsecutive - 1), 0);
        int maxPosY = Mathf.Min(posY + (numConsecutive - 1), playerTokenArray.GetLength(2) - 1);
        for (int y = minPosY; y <= maxPosY; y++) {
            winTokenPos[consecutive, 0] = posX;
            winTokenPos[consecutive, 1] = y;
            consecutive = playerTokenArray[playerTurn, posX, y] ? ++consecutive : 0;
            if (consecutive == numConsecutive) {return true;}     
        }

        // Check positive diagonal
        consecutive = 0;
        int minPosDiag = Mathf.Max(minPosX-posX, minPosY-posY);
        int maxPosDiag = Mathf.Min(maxPosX-posX, maxPosY-posY);
        for (int i = minPosDiag; i <= maxPosDiag; i++) {
            winTokenPos[consecutive, 0] = posX + i;
            winTokenPos[consecutive, 1] = posY + i;
            consecutive = playerTokenArray[playerTurn, posX + i, posY + i] ? ++consecutive : 0;
            if (consecutive == numConsecutive) {return true;}
        }

        // Check negative diagonal
        consecutive = 0;
        int minNegDiag = Mathf.Max(minPosX-posX, posY-maxPosY);
        int maxNegDiag = Mathf.Min(maxPosX-posX, posY-minPosY);
        for (int i = minNegDiag; i <= maxNegDiag; i++) {
            winTokenPos[consecutive, 0] = posX + i;
            winTokenPos[consecutive, 1] = posY - i;
            consecutive = playerTokenArray[playerTurn, posX + i, posY - i] ? ++consecutive : 0;
            if (consecutive == numConsecutive) {return true;}
        }

        return false;
    }

    public int GetPlayerTurn() { return playerTurn; }
    public int[,] GetWinTokenPos() { return winTokenPos; }
}
