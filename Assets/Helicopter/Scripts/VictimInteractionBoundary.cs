using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimInteractionBoundary : MonoBehaviour {
    public bool playerNearby = false;


    void OnTriggerEnter(Collider other)
    {
        playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        playerNearby = false;
    }
}
