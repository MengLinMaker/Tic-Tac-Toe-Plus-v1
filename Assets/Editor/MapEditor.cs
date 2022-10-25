using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Indicate what class this editor is for
[CustomEditor (typeof (GameManager))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();  // Draw default stuff
        GameManager gameManager = target as GameManager;    // Typecast target
        gameManager.GenerateTiles();    // Run for each frame
    }
}
