using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour {
    public Light ambientLight;
    public int numVictims = 3;
    public List<GameObject> victims;
    public List<GameObject> timers;

	// Use this for initialization
	void Start () {
        ambientLight.intensity = (PlayerPrefs.GetInt("Weather") == 1) ? 1f : 0.3f;
        if (numVictims < 3)
        {
            victims[2].SetActive(false);
            timers[2].SetActive(false);
        }
        if (numVictims < 2)
        {
            victims[1].SetActive(false);
            timers[1].SetActive(false);
        }
    }
	
	// Update is called once per frame
}
