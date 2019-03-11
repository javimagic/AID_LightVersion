using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimRecognition : MonoBehaviour {

    public bool victimDetected = false;

    private void Start() {
        victimDetected = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Victim") {
            victimDetected = true;
        }
    }

    /*
    void OnTriggerExit(Collider other) {
        playerNearby = false;
    }
    */
}
