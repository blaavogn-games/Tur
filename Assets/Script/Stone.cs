using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
    private Vector2 startPosition;
    private GridHandler gridHandler;
    public Sprite sprNormal, sprWon;
    private SpriteRenderer spriteRenderer;
    bool won = false;
    
    void Start() {
        gridHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridHandler>();
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    public void reset(){
        transform.position = startPosition;
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Finish"){
            spriteRenderer.sprite = sprWon;
            won = true;
            gridHandler.stoneWon();
        }
    }
    
    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Finish") {
            spriteRenderer.sprite = sprNormal;
            won = false;
        }
    }

    public void snap() {
        Vector2 pos = transform.position;
        pos.x = (pos.x + .25f) - ((pos.x + .25f) % .5f);
        pos.y = (pos.y + .25f) - ((pos.y + .25f) % .5f);
        transform.position = pos;
    }
    public bool isWon() {
        return won;
    }

    public void wonFX() {
        for (int i = 0; i < 10; i++) {
            float rot = ((float)i * 360 / 10);
            Instantiate(Resources.Load("wonFxp"), transform.position, Quaternion.Euler(0,0,rot));
        }
    }
}
