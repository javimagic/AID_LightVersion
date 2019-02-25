using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    public float thrust;
    public float pitchSpeed;
    public float yawSpeed;
    // public float maxPitch;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotateYaw = Input.GetAxis("Horizontal");
        float rotatePitch = Input.GetAxis("Vertical");

        // Debug.Log(rotatePitch);  // Se va poniendo gradualmente en +1 o -1

        Vector3 yawVector = new Vector3(0.0f, yawSpeed * rotateYaw, 0.0f);
        Vector3 pitchVector = new Vector3(pitchSpeed * rotatePitch, 0.0f, 0.0f);
        // Vector3 rotationVector = new Vector3(pitchSpeed * rotatePitch, yawSpeed * rotateYaw, 0.0f);
        gameObject.transform.Rotate(yawVector, Space.World);
        gameObject.transform.Rotate(pitchVector);
        if (Input.GetKey("space"))
        {
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0.0f, thrust, 0.0f));
        }
    }
}