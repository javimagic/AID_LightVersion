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
    public float inverseInertia = 2f;
    
    private float moveSideways = 0.0f, moveUpwards = 0.0f, moveForward = 0.0f, rotateDrone = 0.0f;
    
    void Start () {
		
	}
	
	void FixedUpdate () {

        float leftHorizontal = Input.GetAxis("Horizontal");
        float leftVertical = Input.GetAxis("Vertical");
        float rightHorizontal = Input.GetAxis("PS4_RightAnalogHoriz");
        float rightVertical = Input.GetAxis("PS4_RightAnalogVert");


        moveSideways = Mathf.Lerp(moveSideways, leftHorizontal, Time.fixedDeltaTime * inverseInertia);
        moveForward = Mathf.Lerp(moveForward, leftVertical, Time.fixedDeltaTime * inverseInertia);
        moveUpwards = Mathf.Lerp(moveUpwards, rightVertical * -10f, Time.fixedDeltaTime * inverseInertia);
        rotateDrone = Mathf.Lerp(rotateDrone, rightHorizontal * -10f, Time.fixedDeltaTime * inverseInertia);
        
        GetComponent<Rigidbody>().velocity = (
            Vector3.ProjectOnPlane(transform.right,Vector3.up) * SidewaysForce * moveSideways +
            transform.up * UpwardsForce * moveUpwards +
            Vector3.ProjectOnPlane(transform.forward, Vector3.up) * ForwardForce * moveForward);


        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * perturbationForce);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(tilt * moveForward, transform.localEulerAngles.y - rotateDrone* rotationSpeed, tilt * -moveSideways);
    }
}
