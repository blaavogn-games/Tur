using UnityEngine;

[System.Serializable]
public class Vector2i {

    [SerializeField]
    public int x, y;

    public Vector2i(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public Vector2i(Vector2 v2) {
        this.x = Mathf.RoundToInt(v2.x);
        this.y = Mathf.RoundToInt(v2.y);
    }

    public bool equals(Vector2i v2) {
        return (x == v2.x && y == v2.y);
    }

    public void add(Vector2i v) {
        x += v.x;
        y += v.y;
    }

    public void add(int x, int y) {
        this.x += x;
        this.y += y;
    }

    public Vector2i copy() {
        return new Vector2i(x, y);
    }

    public Vector2 toVector2() {
        return new Vector2(x, y);
    }

    public Vector3 toVector3(float z = -1) {
        return new Vector3(x, y, z);
    }

    public Vector3 toWorldSpace() {
        return new Vector3(x * GridHandler.tileSize, y * GridHandler.tileSize, 0);
    }

    public static Vector2i toCoord(Vector3 v) {
        return new Vector2i((int)((v.x + 8) / GridHandler.tileSize), (int)((v.y + 8) / GridHandler.tileSize));
    }
}
