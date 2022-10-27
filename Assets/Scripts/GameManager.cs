using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int turn = 0;
    [SerializeField] public static GameObject[] tokenPrefabs;
    [SerializeField] public GameObject[] tokenPrefabs_;
    [SerializeField] public static int width, height;
    [SerializeField] public static GameObject[,] tokenArray;


    private void Start() {
        tokenPrefabs = tokenPrefabs_;
    }

    public void PrintFloat(float num) {
        print(num);
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

    private static void SpawnPrefab(GameObject prefab, Vector3 position)
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
