using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
    public float ForwardForce = 3f;
    public float SidewaysForce = 3f;
    public float UpwardsForce = 3f;
    public float tilt = 25;
    public float rotationSpeed = 2.0f;
    public float perturbationForce = 1.0f;

    private float moveSideways = 0.0f, moveUpwards = 0.0f, moveForward = 0.0f, rotateDrone = 0.0f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey("d") || Input.GetKey("a")) {
            if (Input.GetKey("d"))
                moveSideways = Mathf.Lerp(moveSideways, 1, Time.deltaTime * 10);
            if (Input.GetKey("a"))
                moveSideways = Mathf.Lerp(moveSideways, -1, Time.deltaTime * 10);
        } else {
            moveSideways = Mathf.Lerp(moveSideways, 0, Time.deltaTime * 10);
        }

        if (Input.GetKey("i") || Input.GetKey("k")) {
            if (Input.GetKey("i"))
                moveUpwards = Mathf.Lerp(moveUpwards, 1, Time.deltaTime * 10);
            if (Input.GetKey("k"))
                moveUpwards = Mathf.Lerp(moveUpwards, -1, Time.deltaTime * 10);
        } else {
            moveUpwards = Mathf.Lerp(moveUpwards, 0, Time.deltaTime * 10);
        }
        if (Input.GetKey("w") || Input.GetKey("s")) {
            if (Input.GetKey("w"))
                moveForward = Mathf.Lerp(moveForward, 1, Time.deltaTime * 10);
            if (Input.GetKey("s"))
                moveForward = Mathf.Lerp(moveForward, -1, Time.deltaTime * 10);
        } else {
            moveForward = Mathf.Lerp(moveForward, 0, Time.deltaTime * 10);
        }
        if (Input.GetKey("j") || Input.GetKey("l")) {
            if (Input.GetKey("j"))
                rotateDrone = Mathf.Lerp(rotateDrone, 1, Time.deltaTime * 10);
            if (Input.GetKey("l"))
                rotateDrone = Mathf.Lerp(rotateDrone, -1, Time.deltaTime * 10);
        } else {
            rotateDrone = Mathf.Lerp(rotateDrone, 0, Time.deltaTime * 10);
        }
        

        // GetComponent<Rigidbody>().velocity = (new Vector3(SidewaysForce * moveSideways, UpwardsForce * moveUpwards, ForwardForce * moveForward));
        // GetComponent<Rigidbody>().velocity = transform.right * SidewaysForce * moveSideways + transform.up * UpwardsForce * moveUpwards + transform.forward * ForwardForce * moveForward;
        GetComponent<Rigidbody>().velocity = (
            Vector3.ProjectOnPlane(transform.right,Vector3.up) * SidewaysForce * moveSideways +
            transform.up * UpwardsForce * moveUpwards +
            Vector3.ProjectOnPlane(transform.forward, Vector3.up) * ForwardForce * moveForward);


        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * perturbationForce);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(tilt * moveForward, transform.localEulerAngles.y - rotateDrone* rotationSpeed, tilt * -moveSideways);
    }
}
