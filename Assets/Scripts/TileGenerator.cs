using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable] public class TileEvent : UnityEvent<Tile> {}
[System.Serializable] public class TileGeneratorEvent : UnityEvent<TileGenerator> { }



public class TileGenerator : MonoBehaviour {
  [SerializeField] private Tile tilePrefab;
  [SerializeField] private float tileSize;
  [SerializeField] public int width, height;
  [SerializeField] private Camera camera;
  [SerializeField] public TileEvent mouseDownEvent;
  [SerializeField] public TileGeneratorEvent tileGeneratorEvent;
  private Tile[,] tiles;




  void Start() {
    tiles = new Tile[width, height];
    GenerateTiles();
    tileGeneratorEvent?.Invoke(this);
  }

  // Limit inspector inputs
  [ExecuteInEditMode] private void OnValidate() {
    width = (width < 3) ? 3 : width;
    height = (height < 3) ? 3 : height;
    tileSize = Mathf.Clamp(tileSize,0.5f,1f);
  }

  public void GenerateTiles() {
    // GameObject to hold tiles
    string tileGroupName = "Generated Tiles";
    if (GameObject.Find(tileGroupName)) {
      DestroyImmediate(GameObject.Find(tileGroupName).gameObject);
    }
    Transform tileMap = new GameObject(tileGroupName).transform;
    
    // Place tile holder GameObject under TileGenerator object
    tileMap.transform.parent = this.transform;

    // Generate game tiles
    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        // Make tile object with specified position and object name
        var tileObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
        tileObject.name = $"Tile_{x}_{y}";

        // Set offset color
        var isOffset = ((x + y) % 2 == 1);
        tileObject.Init(isOffset);
        tileObject.transform.localScale = new Vector3(tileSize, tileSize, 1);
        tileObject.transform.parent = tileMap.transform;
        tiles[x, y] = tileObject;
      }
    }
  }

  private void Update() {
    CameraTransformation(width, height);
  }

  private void CameraTransformation(float width, float height) {
    camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -(width + height));
    float orthographicHeight = height * 0.5f / 0.8f;
    float orthographicWidth = width * Screen.height / Screen.width * 0.5f / 0.8f;
    camera.orthographicSize = Mathf.Max(orthographicHeight, orthographicWidth);
  }

  public void OnMouseDown(Tile tile) {
    mouseDownEvent?.Invoke(tile);
  }

  public void SetWinColor(GameManager gameManager) {
    int[,] pos = gameManager.GetWinTokenPos();
    for (int i = 0; i < pos.GetLength(0); i++) {
      tiles[pos[i,0], pos[i,1]].SetWinColor();
    }
  }
}
