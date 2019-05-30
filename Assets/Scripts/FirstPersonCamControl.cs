using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamControl : MonoBehaviour
{
    public Transform goalPos;
    public float camSensitivity = 500f;

    // Start is called before the first frame update
    void Start()
    {
    }

    
    // Update is called once per frame
    void Update()
    {
        // /*
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            goalPos.rotation * Quaternion.Euler(
                Input.GetAxis("PS4_RightAnalogVert") * camSensitivity,
                Input.GetAxis("PS4_RightAnalogHoriz") * camSensitivity,
                0f),
            Time.deltaTime);
        // */
        Debug.Log(goalPos.rotation);
    }
    
    }
