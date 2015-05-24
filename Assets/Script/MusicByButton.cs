using UnityEngine;
using System.Collections;

//LAV ET BUTTON ITNERFACE NÆSTE GNAG?!?!?!?!?!?!?!?!?!?!?!?!?!?!?
public class MusicByButton : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite normal, hover;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse.x > transform.position.x - 1f && mouse.x < transform.position.x + 1f &&
            mouse.y > transform.position.y - 0.2f && mouse.y < transform.position.y + 0.2f) {
            if (Input.GetMouseButtonDown(0)) {
                Application.OpenURL("http://opengameart.org/users/jan125");
            }
            spriteRenderer.sprite = hover;
        } else {
            spriteRenderer.sprite = normal;
        }
    }
}
