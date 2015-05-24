using UnityEngine;
using System.Collections;

public class WonFX : MonoBehaviour {
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(2.4f * Time.deltaTime, 0, 0);
        transform.Rotate(new Vector3(0, 0, 120 * Time.deltaTime));
        transform.localScale += new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 0);
       // transf
    }
}
