using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

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
        GameManager.TileClicked(this);
    }
}
