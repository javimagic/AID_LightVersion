using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRotorSpeedChanger : MonoBehaviour
{
    public List<GameObject> rotors;
    public float baseSpeed = 25f;
    public float extraSpeed = 1.5f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject rotor in rotors) {
            rotor.GetComponent<ContinuousRotation>().rotationSpeed = baseSpeed + gameObject.GetComponent<Rigidbody>().velocity.y * extraSpeed;
        }
    }
}
