using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

    public Tile.Types type;
    SpriteRenderer spriteRenderer;
    public Sprite normal, hover;
    public bool newGame = false;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse.x > transform.position.x - 1f && mouse.x < transform.position.x + 1f &&
            mouse.y > transform.position.y - 0.25f && mouse.y < transform.position.y + 0.25f) {
            if (Input.GetMouseButtonDown(0)) {
                if(newGame)
                    PlayerPrefs.SetInt("level", 1);
                int level = PlayerPrefs.GetInt("level",1);
                Application.LoadLevel(level);
            }
            spriteRenderer.sprite = hover;
        } else {
            spriteRenderer.sprite = normal;
        }
    }
}
