using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {
    public Sprite sprDef, left, right, up, down;
    private Tile.Types type = Tile.Types.DEFAULT;
    private bool placeable = false;
    private GridHandler gridHandler;
    private readonly Color colNormal = new Color(1, 1, 1, .5f), colIllegal = new Color(1, 0, 0, .0f);
    Player player;
    GameObject[] stones, goals;
    SpriteRenderer spriteRenderer;
    public AudioClip placeArrowSFX;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridHandler>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stones = GameObject.FindGameObjectsWithTag("Respawn");
        goals = GameObject.FindGameObjectsWithTag("Finish");
    }
	
	void Update () {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (gridHandler.getTile(mouse) == null || gridHandler.getGameState() != GridHandler.gameStates.STOPPED ) {
            transform.position = new Vector2(-100, -100);
            placeable = false;
        } else {
            mouse.x = (mouse.x + .25f) - ((mouse.x + .25f) % .5f);
            mouse.y = (mouse.y + .25f) - ((mouse.y + .25f) % .5f);

            transform.position = mouse;
            placeable = true;

            bool aloud = true;
            if((Vector2)player.transform.position == mouse)
                aloud = false;
            foreach(GameObject g in goals)
                if((Vector2)g.transform.position == mouse)
                    aloud = false;
            foreach(GameObject s in stones)
                if((Vector2)s.transform.position == mouse)
                    aloud = false;

            if (gridHandler.getTile(mouse).getType() == Tile.Types.WALL || !aloud) {
                spriteRenderer.color = colIllegal;
                placeable = false;
            }else{
                spriteRenderer.color = colNormal;
            } 
            
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            setType(Tile.Types.UP);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            setType(Tile.Types.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            setType(Tile.Types.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            setType(Tile.Types.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.E) ) {
            setType(Tile.Types.DEFAULT);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            scrollType(-1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            scrollType(1);
        }
        if (placeable) {
            if (Input.GetMouseButtonDown(0)) {
                Tile t = gridHandler.getTile(transform.position);
                if(t.getType() != type){
                    t.setType(type);
                    audio.PlayOneShot(placeArrowSFX);
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                setType(Tile.Types.DEFAULT);
                Tile t = gridHandler.getTile(transform.position);
                if (t.getType() != Tile.Types.DEFAULT) {
                    t.setType(type);
                    audio.PlayOneShot(placeArrowSFX);
                }
            }
        }
	}

    public void scrollType(int dir) {
        int typeVal = -1;
        switch (type) { //Forgive me lord for I have sinned 
            case Tile.Types.DOWN:
                typeVal = 4; break;
            case Tile.Types.RIGHT:
                typeVal = 5; break;
            case Tile.Types.UP:
                typeVal = 6; break;
            case Tile.Types.LEFT:
            default:
                typeVal = 7; break;
        }
        typeVal = (typeVal + dir) % 4;

        switch (typeVal) {
            case 0:
                setType(Tile.Types.DOWN);
                break;
            case 1:
                setType(Tile.Types.RIGHT);
                break;
            case 2:
                setType(Tile.Types.UP);
                break;
            case 3:
                setType(Tile.Types.LEFT);
                break;
        }
    }

    public void setType(Tile.Types t) {
        type = t;
        switch (t) {
            case Tile.Types.RIGHT:
                spriteRenderer.sprite = right;
                break;
            case Tile.Types.LEFT:
                spriteRenderer.sprite = left;
                break;
            case Tile.Types.UP:
                spriteRenderer.sprite = up;
                break;
            case Tile.Types.DOWN:
                spriteRenderer.sprite = down;
                break;
            case Tile.Types.DEFAULT:
                spriteRenderer.sprite = sprDef;
                break;
            case Tile.Types.WALL:

                break;
        }
    }
}
