using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {
    public bool droneActive = false;
    public float ForwardForce = 3f;
    public float SidewaysForce = 3f;
    public float UpwardsForce = 3f;
    public float tilt = 25;
    public float rotationSpeed = 2.0f;
    public float perturbationForce = 1.0f;
    public float inverseInertia = 3f;
    public float noiseFrec = 2.5f;
    public float noiseMoveConst = 2f;

    private float desfY, desfZ;
    private float sensL, sensR;
    
    private float moveSideways = 0.0f, moveUpwards = 0.0f, moveForward = 0.0f, rotateDrone = 0.0f;
    
    void Start () {
        desfY = Random.Range(0f, 2 * Mathf.PI);
        desfZ = Random.Range(0f, 2 * Mathf.PI);
        sensL = PlayerPrefs.GetFloat("SensDronL");
        sensR = PlayerPrefs.GetFloat("SensDronR");
    }
	
	void FixedUpdate () {

        float leftHorizontal = (droneActive)? Input.GetAxis("Horizontal") : 0f;
        float leftVertical = (droneActive) ? Input.GetAxis("Vertical") : 0f;
        float rightHorizontal = (droneActive) ? Input.GetAxis("PS4_RightAnalogHoriz") : 0f;
        float rightVertical = (droneActive) ? Input.GetAxis("PS4_RightAnalogVert") : 0f;
        /*
        Debug.Log(
            "H_Sens = " + PlayerPrefs.GetFloat("SensHeli") +
            ". DronL_Sens = " + PlayerPrefs.GetFloat("SensDronL") +
            ". DronR_Sens = " + PlayerPrefs.GetFloat("SensDronR")
            );
        */

        Vector3 noiseMove = noiseMoveConst * new Vector3(Mathf.Sin(noiseFrec * Time.time * 1.3f), Mathf.Sin(noiseFrec * Time.time + desfY), Mathf.Sin(noiseFrec * Time.time * 1.8f + desfZ));
        

        moveSideways = Mathf.Lerp(moveSideways, sensL * leftHorizontal, Time.fixedDeltaTime * inverseInertia) + noiseMove.z * 0.001f;
        moveForward = Mathf.Lerp(moveForward, sensL * leftVertical, Time.fixedDeltaTime * inverseInertia) + noiseMove.x * 0.001f;
        moveUpwards = Mathf.Lerp(moveUpwards, sensR * rightVertical * -10f, Time.fixedDeltaTime * inverseInertia) + noiseMove.y * 0.001f;
        rotateDrone = Mathf.Lerp(rotateDrone, sensR * rightHorizontal * -10f, Time.fixedDeltaTime * inverseInertia);

        moveSideways = Mathf.Clamp(moveSideways, -1, 1);
        moveForward = Mathf.Clamp(moveForward, -1, 1);
        moveUpwards = Mathf.Clamp(moveUpwards, -1, 1);

        GetComponent<Rigidbody>().velocity = (
            Vector3.ProjectOnPlane(transform.right,Vector3.up) * SidewaysForce * moveSideways +
            transform.up * UpwardsForce * moveUpwards +
            Vector3.ProjectOnPlane(transform.forward, Vector3.up) * ForwardForce * moveForward);
        
        // GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * perturbationForce);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(tilt * moveForward, transform.localEulerAngles.y - rotateDrone* rotationSpeed, tilt * -moveSideways);
        // GetComponent<Rigidbody>().rotation *= Quaternion.Euler(noiseRot);
    }
}
