using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int turn = 0;
    [SerializeField] private  GameObject[] tokenPrefabs;
    [SerializeField] private int width, height;
    private GameObject[,] tokenArray;
    void Start()
    {   
        tokenArray = new GameObject[width,height];
    }

    public void TileClicked(Tile tile)
    {
        // Get tile position
        Vector3 position = tile.transform.position;

        if (tokenArray[(int) position.x, (int) position.y] == null) {
            // Calculating turn of player
            int playerTurn = turn%tokenPrefabs.Length;
            turn++;
            
            // Selecting prefab to spawn
            GameObject prefab = tokenPrefabs[playerTurn];
            SpawnPrefab(prefab, position);
            tokenArray[(int) position.x, (int) position.y] = prefab;
            print(tokenArray);
        }
    }

    private void SpawnPrefab(GameObject prefab, Vector3 position)
    {
        position.z--;
        Instantiate(prefab, position, Quaternion.identity);
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
