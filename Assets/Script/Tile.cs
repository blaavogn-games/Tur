using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile : MonoBehaviour {
    public enum Types { DEFAULT, WALL, LEFT, UP, RIGHT, DOWN};
    public Sprite sprDefault, wall, left, up, down, right;
    Sprite spr;
    Quaternion rot;
    [SerializeField]
    public Vector2i coordinate;
    [SerializeField]
    private Types type, initType;

    public void Start() {
        rot = transform.rotation;
    }

    public void run() {
        spr = GetComponent<SpriteRenderer>().sprite;
        initType = type;
    }

    public void reset() {
        transform.rotation = rot;
        setType(initType);
        GetComponent<SpriteRenderer>().sprite = spr;
    }

    public void setCoordinate(Vector2i coordinate) {
        this.coordinate = coordinate;
    }

    public void setColor(Color c) {
        GetComponent<SpriteRenderer>().color = c;
    }
    
    public void setType(int rotation) {
        int newRotation = (rotation + typeToRotation(type)) % 360;
        switch (newRotation) {
            case 0:
                setType(Types.UP);
                break;
            case 90:
                setType(Types.LEFT);
                break;
            case 180:
                setType(Types.DOWN);
                break;
            case 270:
                setType(Types.RIGHT);
                break;
        }
    }
    
    public void setType(Types type) {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        this.type = type;
        switch (type) {
            case Types.DEFAULT:
                GetComponent<SpriteRenderer>().sprite = sprDefault;
                sprRenderer.sprite = sprDefault;
                break;
            case Types.WALL:
                GetComponent<SpriteRenderer>().sprite = wall;
            //    sprRenderer.sprite = wall;
                break;
            case Types.LEFT:
                GetComponent<SpriteRenderer>().sprite = left;
                sprRenderer.sprite = left;
                break;
            case Types.RIGHT:
                GetComponent<SpriteRenderer>().sprite = right;
                sprRenderer.sprite = right;
                break;
            case Types.UP:
                GetComponent<SpriteRenderer>().sprite = up;
                sprRenderer.sprite = up;
                break;
            case Types.DOWN:
                GetComponent<SpriteRenderer>().sprite = down;
                sprRenderer.sprite = down;
                break;
        }
    }

    public void toggle() {
    }

    public Types getType() {
        return type;
    }

    public static int typeToRotation(Types t) {
        switch (t) {
            case Types.UP:
                return 0;
            case Types.DOWN:
                return 180;
            case Types.LEFT:
                return 90;
            case Types.RIGHT:
                return 270;
        }
        return 0;
    }
}
