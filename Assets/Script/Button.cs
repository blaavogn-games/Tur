using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Tile.Types type;
    SpriteRenderer spriteRenderer;
    public Highlight highlight;
    public Sprite normal, hover;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
    void Update () {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse.x > transform.position.x - 0.25f && mouse.x < transform.position.x + 0.25f &&
            mouse.y > transform.position.y - 0.25f && mouse.y < transform.position.y + 0.25f) {
            if (Input.GetMouseButtonDown(0)) {
                highlight.setType(type);
            }
            spriteRenderer.sprite = hover;
        } else {
            spriteRenderer.sprite = normal;
        }
	}
}
