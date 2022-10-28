using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private GameObject tokenParent;
    private int turn = 0;
    private GameObject[,] prefabArray;



    public void InitPrefabArray(TileGenerator tileGenerator) {
        prefabArray = new GameObject[tileGenerator.width, tileGenerator.height];
    }

    public void TileClicked(Tile tile)
    {
        // Get tile position
        Vector3 position = tile.transform.position;

        if (prefabIsNullAtPos(position.x, position.y)) {
            // Calculating turn of player
            int playerTurn = turn%prefabs.Length;
            turn++;
            
            // Selecting prefab to spawn
            GameObject prefab = prefabs[playerTurn];
            SpawnPrefab(prefab, position);
            prefabArray[(int) position.x, (int) position.y] = prefab;
        }
    }
    private void SetprefabAtPos(float posX, float posY, GameObject prefab) {
        prefabArray[(int) posX, (int) posY] = prefab;
    }

    private bool prefabIsNullAtPos(float posX, float posY) {
        return prefabArray[(int) posX, (int) posY] == null;
    }

    private void SpawnPrefab(GameObject prefab, Vector3 position)
    {
        position.z--;
        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
        gameObject.transform.parent = tokenParent.transform;
    }

    /**
    private Vector3 NormalisedPosition(Tile tile)
    {
        Vector3 position = tile.transform.position;
        position.x = Mathf.RoundToInt( position.x / tile.transform.lossyScale.x);
        position.y = Mathf.RoundToInt( position.y / tile.transform.lossyScale.y);
        return position;
    }**/

}
