using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor {
    Tile tile;

    void OnEnable() {
        tile = (Tile)target;
    }

    void OnSceneGUI() {
        Event e = Event.current;

        if (e.type == EventType.keyUp) {
            switch (e.keyCode) {
                case KeyCode.N:
                    tile.setType(Tile.Types.DEFAULT);
                    break;
                case KeyCode.M:
                    tile.setType(Tile.Types.WALL);
                    break;
                case KeyCode.W:
                    selectRelative(0, 1);
                    break;
                case KeyCode.S:
                    selectRelative(0, -1);
                    break;
                case KeyCode.A:
                    selectRelative(-1, 0);
                    break;
                case KeyCode.D:
                    selectRelative(1, 0);
                    break;
            }
        }
    }

    void selectRelative(int x, int y) {
        string wishedName = (tile.coordinate.x + x) + "," + (tile.coordinate.y + y);
        GameObject wishedObject = GameObject.Find(wishedName);

        if (wishedObject != null) {
            GameObject[] newSelection = { wishedObject };
            Selection.objects = newSelection;
        }
    }
}
