using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class DifficultySelector : MonoBehaviour {
    public Text difficultyLabel;
    Slider slider;

    // Use this for initialization
    void Start () {
        slider = GetComponentInParent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SliderValueChanged() {
        if (slider.value == 0) {
            GameConfig.difficulty = 1;
            difficultyLabel.text = "Easy";
            difficultyLabel.color = Color.green;
        } else if (slider.value == 1) {
            GameConfig.difficulty = 2;
            difficultyLabel.text = "Medium";
            difficultyLabel.color = Color.yellow;
        } else {
            GameConfig.difficulty = 3;
            difficultyLabel.text = "Hard";
            difficultyLabel.color = Color.red;
        }
    }

}
