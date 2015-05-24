using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Stone))]
public class StoneEditor : Editor {
    Stone stone;

    void OnEnable() {
        stone = (Stone)target;
        stone.snap();        
    }

    void OnSceneGUI() {
        Event e = Event.current;

        if (e.type == EventType.MouseUp) {
            stone.snap();
        }
    }
}
