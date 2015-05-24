using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class GridHandler : MonoBehaviour {
    public enum gameStates { RUN, PAUSE, STOPPED };
    private gameStates gameState = gameStates.STOPPED;
    Player player;
    public RunButton runButton;
    private HashSet<string> gridStates = new HashSet<string>(); 
    public const int gridX = 11, gridY = 8;
    public const float tileSize = .5f;
    [SerializeField]
    GameObject[] grid = new GameObject[gridX * gridY];
    GameObject[] stones;
    private bool isWinning = false;
    private float winTime = 1.5f;
    public bool lastLavel = false;
    public AudioClip sfxWin, smallWin;

    // Use this for initialization
    void Start() {
        stones = GameObject.FindGameObjectsWithTag("Respawn");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (GameObject.FindGameObjectWithTag("Musik") == null)
            Instantiate(Resources.Load("MusicPlayer"));
    }
    void Update() {
        if (isWinning) {
            winTime -= Time.deltaTime;
            if (winTime < 0) {

                if (Application.loadedLevel == 13) {
                    PlayerPrefs.SetInt("level", 12);
                    Application.LoadLevel(0);
                } else {
                    PlayerPrefs.SetInt("level", Application.loadedLevel + 1);
                    Application.LoadLevel(Application.loadedLevel + 1);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.LoadLevel(0);
        }
    }
    public void stoneWon() {
        foreach (GameObject g in stones) {
            Stone s = g.GetComponent<Stone>();
            if (!s.isWon()) {
                audio.PlayOneShot(smallWin);
                return;
            }
        }
        foreach (GameObject g in stones) {
            Stone s = g.GetComponent<Stone>();
            s.wonFX();
        }
        audio.PlayOneShot(sfxWin);
        isWinning = true;
        toggleGameState();
    }
        

    public void clearGrid() {
        for (int i = 0; i < grid.Length; i++) {
            if (grid[i] != null) {
                Object.DestroyImmediate(grid[i]);
            }
        }

        grid = new GameObject[gridX * gridY];
        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                GameObject tempGO = (GameObject) Instantiate(Resources.Load("grid/Tile"), new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
                tempGO.transform.name = x + "," + y;
                tempGO.transform.parent = transform;
                Tile tile = tempGO.GetComponent<Tile>();
                tile.setCoordinate(new Vector2i(x, y));
                grid[x + y * gridX] = tempGO;
            }
        }
    }

    public Tile getTile(int x, int y) {
        if (x >= 0 && x < gridX && y >= 0 && y < gridY) {
            return grid[x + y * gridX].GetComponent<Tile>();
        }
        return null;
    }
    public Tile getTile(Vector2i coordinate) {
        return getTile(coordinate.x, coordinate.y);
    }

    public Tile getTile(Vector2 v) {
        if (v.x < -0.25f || v.x > tileSize * gridX - 0.25f ||
            v.y < -0.25f || v.y > tileSize * gridY - 0.25f)
            return null;

        int x = (int)((v.x + .25f) / .5f);
        int y = (int)((v.y + .25f) / .5f);
        return grid[x + gridX * y].GetComponent<Tile>();
    }

    public gameStates getGameState(){
        return gameState;
    }

    public void toggleGameState() {
        switch(gameState){
            case gameStates.STOPPED:
                for (int x = 0; x < gridX; x++) {
                    for (int y = 0; y < gridY; y++) {
                        grid[x + gridX * y].GetComponent<Tile>().run();
                    }
                }
                gameState = gameStates.RUN;
                player.findTarget();
                break;
            case gameStates.RUN:
                gameState = gameStates.PAUSE;
                break;
            case gameStates.PAUSE:
                for (int x = 0; x < gridX; x++) {
                    for (int y = 0; y < gridY; y++) {
                        grid[x + gridX * y].GetComponent<Tile>().reset();
                    }
                }
                Camera.main.backgroundColor = new Color32(2,0,42,0);
                gameState = gameStates.STOPPED;
                player.reset();
                gridStates.Clear();
                foreach (GameObject g in stones) {
                    g.GetComponent<Stone>().reset();
                }
                break;
        }
        runButton.changeState(gameState);
    }

    public void saveGameState(Vector2 position, Vector2 direction) {
        string state = "";

        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                state += typeToLetter(grid[x + gridX * y].GetComponent<Tile>().getType());
            }
        }
        foreach(GameObject g in stones){
            state += g.transform.position.x;
            state += g.transform.position.y;
        }
        state += position.x;
        state += position.y;
        state += direction.x;
        state += direction.y;
        
        if (gridStates.Contains(state)) {
            runButton.isLooping();
            if (!isWinning && lastLavel) {
                isWinning = true;
                winTime = 5f;
                audio.PlayOneShot(sfxWin);
            }
        } else {
            gridStates.Add(state);
        }
    }

    public string typeToLetter(Tile.Types t) {
        switch (t) {
            case Tile.Types.DEFAULT:
                return "N";
            case Tile.Types.UP:
                return "U";
            case Tile.Types.DOWN:
                return "D";
            case Tile.Types.LEFT:
                return "L";
            case Tile.Types.RIGHT:
                return "R";
            case Tile.Types.WALL:
                return "W";
        }
        return "_";
    }
}
