using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour {
    public Light ambientLight;

	// Use this for initialization
	void Start () {
        ambientLight.intensity = (PlayerPrefs.GetInt("Weather") == 1) ? 1f : 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
