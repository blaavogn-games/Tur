using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GridHandler))]
public class GridHandlerEditor : Editor {
    GridHandler gridHandler;
    
    void OnAwake() {
        gridHandler = (GridHandler)target;
    }

    void OnEnable() {
        gridHandler = (GridHandler)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

    
        if (GUILayout.Button("Create Grid", GUILayout.Height(41), GUILayout.Width(160))) {
            gridHandler.clearGrid();
        }
    }

}
