using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMover : MonoBehaviour {
    private float rotateSpeed = 2;
    private float forwardSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rotationInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0.0f, rotateSpeed * rotationInput, 0.0f), Space.World);
        transform.Translate(0.0f, 0.0f, forwardInput * forwardSpeed);
    }
}
