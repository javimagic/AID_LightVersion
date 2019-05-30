using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera explosionCamera;

    // Use this for initialization
    void Start()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("1") || Input.GetAxis("Flechas_Horiz") == -1) {
            camera1.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
        if (Input.GetKeyDown("2") || Input.GetAxis("Flechas_Vert") == 1) {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }
        if (Input.GetKeyDown("3") || Input.GetAxis("Flechas_Vert") == -1) {
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
        /*
        else
        {
            explosionCamera.enabled = true;
        }
        */
    }
}