using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (audio.isPlaying)
                audio.Pause();
            else
                audio.Play();
        }
    }
}
