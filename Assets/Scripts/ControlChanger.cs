using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChanger : MonoBehaviour
{
    public GameObject helicopter;
    public GameObject aproxZone;
    public GameObject drone;
    public Camera heliCam;
    public Camera droneCam;
    public GameObject victimReckon;
    public float lastHeight;
    public bool controllingHeli = true;
    private bool victimDetected = false;
    private bool insideAproxZone = false;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        drone.GetComponent<DroneController>().enabled = false;
        droneCam.enabled = false;
        drone.SetActive(false);
        offset = drone.transform.position - helicopter.transform.position;
    }

    // Update is called once per frame


    void controlDrone()
    {
        helicopter.GetComponent<HelicopterController>().controllingThisHelicopter = false;
        lastHeight = helicopter.transform.position.y;
        if (!drone.activeSelf) {
            drone.transform.position = helicopter.transform.position + offset;
            drone.transform.localEulerAngles = new Vector3(
                drone.transform.localEulerAngles.x,
                helicopter.transform.localEulerAngles.y,
                drone.transform.localEulerAngles.x
                );
        }
        drone.SetActive(true);
        drone.GetComponent<DroneController>().enabled = true;
        heliCam.enabled = false;
        droneCam.enabled = true;
        controllingHeli = false;
        return;
    }

    void controlHelicopter()
    {
        helicopter.GetComponent<HelicopterController>().controllingThisHelicopter = true;
        drone.GetComponent<DroneController>().enabled = false;
        heliCam.enabled = true;
        droneCam.enabled = false;
        controllingHeli = true;
        return;
    }


    void FixedUpdate()
    {
        if (controllingHeli && (Input.GetButton("PS4_triangle") || Input.GetKey("r")))
        {
            // Debug.Log("Controlling drone now");
            controlDrone();
        }
        if (!(controllingHeli) && (Input.GetButton("PS4_square") || Input.GetKey("t")))
        {
            // Debug.Log("Controlling helicopter now");
            controlHelicopter();
        }
        if (!victimDetected)
        {
            if (victimReckon.GetComponent<VictimRecognition>().victimDetected)
            {
                victimDetected = true;
                // controlHelicopter();
            }
        }
        if (!insideAproxZone)
        {
            if (aproxZone.GetComponent<VictimInteractionBoundary>().playerNearby)
            {
                insideAproxZone = true;
                // controlDrone();
            }
        }
    }
}

