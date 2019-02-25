using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    public Camera camera;
    public Camera camera2;
    public Camera camera3;
    // Use this for initialization
    void Start()
    {
        camera.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            camera.enabled = true;
            camera2.enabled = false;
            camera3.enabled = false;
        }
        if (Input.GetKeyDown("2"))
        {
            camera.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
        }
        if (Input.GetKeyDown("3"))
        {
            camera.enabled = false;
            camera2.enabled = false;
            camera3.enabled = true;
        }
    }
}