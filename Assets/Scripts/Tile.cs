using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    public Transform parent;
    private GameManager gameManager;

    private void Start() {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponentInParent<GameManager>();
    }

    public void Init(bool isOffset) {
        renderer.color = isOffset ? offsetColor: baseColor;
    }

    void OnMouseOver() {
        highlight.SetActive(true);
    } 

    void OnMouseExit() {
        highlight.SetActive(false);
    }

    void OnMouseDown() {
        gameManager.TileClicked(this);
    }
}
