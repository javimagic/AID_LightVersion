using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class WeatherSelector : MonoBehaviour {
    public Button otherButton1, otherButton2;
    public int climate; // 1=sun, 2=rain, 3=snow

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked() {
        GetComponentInParent<Button>().interactable = false;
        otherButton1.interactable = true;
        otherButton2.interactable = true;
        GameConfig.weather = climate;
        // Set weather
        // selfButton.GetComponent<Button>().interactable = false;
    }

}
