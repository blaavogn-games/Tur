using UnityEngine;
using System.Collections;

public class RunButton : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite[] current = new Sprite[2], run = new Sprite[2], pause = new Sprite[2], reset = new Sprite[2], loop = new Sprite[2];
    GridHandler gridHandler;

	// Use this for initialization
	void Start () {
        gridHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        current = run;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse.x > transform.position.x - 0.5f && mouse.x < transform.position.x + 0.5f &&
            mouse.y > transform.position.y - 0.25f && mouse.y < transform.position.y + 0.25f) {
            spriteRenderer.sprite = current[1];
            if (Input.GetMouseButtonDown(0)) {
                gridHandler.toggleGameState();
            }
        } else {
            spriteRenderer.sprite = current[0];
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            gridHandler.toggleGameState();

            switch (gridHandler.getGameState()) {
                case GridHandler.gameStates.STOPPED:
                    break;
                case GridHandler.gameStates.RUN:
                    break;
            }
        }
	}

    public void changeState(GridHandler.gameStates state) {
        switch (state) {
            case GridHandler.gameStates.PAUSE:
                current = reset;
                break;
            case GridHandler.gameStates.STOPPED:
                current = run;
                break;
            case GridHandler.gameStates.RUN:
                current = pause;
                break;
        }
    }

    public void isLooping() {
        current = loop;
    }
}
