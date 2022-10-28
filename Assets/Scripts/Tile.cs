using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour {
    [SerializeField] private Color baseColor, offsetColor, winColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;



    public void Init(bool isOffset) {
        renderer.color = isOffset ? offsetColor: baseColor;
    }

    public void SetWinColor() {
        renderer.color = winColor;
    }

    void OnMouseOver() {
        highlight.SetActive(true);
    } 

    void OnMouseExit() {
        highlight.SetActive(false);
    }

    void OnMouseDown() {
        FindObjectOfType<TileGenerator>().OnMouseDown(this);
    }
}