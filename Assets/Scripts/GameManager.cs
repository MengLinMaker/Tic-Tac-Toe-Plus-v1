using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int turn = 0;
    [SerializeField] private  GameObject[] tokenPrefabs;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float tileSize;
    [SerializeField] private int width, height;
    private GameObject[,] tokenArray;
    [SerializeField] private Transform camera;

    void Start()
    {   
        tokenArray = new GameObject[width,height];
        GenerateTiles();
    }

    public void GenerateTiles() {
        // GameObject to hold tiles
        string tileGroupName = "Generated Tiles";
        if (GameObject.Find(tileGroupName))
        {
            DestroyImmediate(GameObject.Find(tileGroupName).gameObject);
        }
        Transform tileMap = new GameObject(tileGroupName).transform;

        // Generate game tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Make tile object at position and rename
                var tileObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tileObject.name = $"Tile_{x}_{y}";

                // Set offset color
                var isOffset = ((x + y) % 2 == 1);
                tileObject.Init(isOffset);
                tileObject.transform.localScale = new Vector3(tileSize, tileSize, 1);
                tileObject.transform.parent = tileMap.transform;
            }
        }
        camera.transform.position = new Vector3((float)width/2-0.5f, (float)height/2-0.5f, -(width+height));
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
