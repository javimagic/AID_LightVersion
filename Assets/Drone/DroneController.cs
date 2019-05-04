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
    public float inverseInertia = 3f;
    public float noiseFrec = 2.5f;
    public float noiseMoveConst = 5f;

    private float desfY, desfZ;
    
    private float moveSideways = 0.0f, moveUpwards = 0.0f, moveForward = 0.0f, rotateDrone = 0.0f;
    
    void Start () {
        desfY = Random.Range(0f, 2 * Mathf.PI);
        desfZ = Random.Range(0f, 2 * Mathf.PI);
    }
	
	void FixedUpdate () {

        float leftHorizontal = Input.GetAxis("Horizontal");
        float leftVertical = Input.GetAxis("Vertical");
        float rightHorizontal = Input.GetAxis("PS4_RightAnalogHoriz");
        float rightVertical = Input.GetAxis("PS4_RightAnalogVert");

        Vector3 noiseMove = noiseMoveConst * new Vector3(Mathf.Sin(noiseFrec * Time.time * 1.3f), Mathf.Sin(noiseFrec * Time.time + desfY), Mathf.Sin(noiseFrec * Time.time * 1.8f + desfZ));



        moveSideways = Mathf.Lerp(moveSideways, leftHorizontal, Time.fixedDeltaTime * inverseInertia) + noiseMove.z * 0.001f;
        moveForward = Mathf.Lerp(moveForward, leftVertical, Time.fixedDeltaTime * inverseInertia) + noiseMove.x * 0.001f;
        moveUpwards = Mathf.Lerp(moveUpwards, rightVertical * -10f, Time.fixedDeltaTime * inverseInertia) + noiseMove.y * 0.001f;
        rotateDrone = Mathf.Lerp(rotateDrone, rightHorizontal * -10f, Time.fixedDeltaTime * inverseInertia);
        
        GetComponent<Rigidbody>().velocity = (
            Vector3.ProjectOnPlane(transform.right,Vector3.up) * SidewaysForce * moveSideways +
            transform.up * UpwardsForce * moveUpwards +
            Vector3.ProjectOnPlane(transform.forward, Vector3.up) * ForwardForce * moveForward);
        
        // GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * perturbationForce);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(tilt * moveForward, transform.localEulerAngles.y - rotateDrone* rotationSpeed, tilt * -moveSideways);
        // GetComponent<Rigidbody>().rotation *= Quaternion.Euler(noiseRot);
    }
}
