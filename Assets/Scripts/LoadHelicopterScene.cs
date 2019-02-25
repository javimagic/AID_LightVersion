using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHelicopterScene : MonoBehaviour {
    private void Update() {
        if (Input.GetKey("space")) {
            SceneManager.LoadScene("Field");
        }
        if (Input.GetKey("up")) {
            Debug.Log(GameConfig.difficulty);
        }
        if (Input.GetKey("down")) {
            Debug.Log(GameConfig.weather);
        }
    }
}
