using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAxesMinimapCamera : MonoBehaviour
{
    public bool fixXMove = false;
    public bool fixYMove = true;
    public bool fixZMove = false;
    public bool fixXRot = true;
    public bool fixYRot = false;
    public bool fixZRot = true;
    // public bool lookDown = true;
    private Vector3 oriPos;
    private Vector3 oriRot;

    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
        oriRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            (fixXMove)? oriPos.x : transform.position.x,
            (fixYMove) ? oriPos.y : transform.position.y,
            (fixZMove) ? oriPos.z : transform.position.z
            );
        transform.rotation = Quaternion.Euler(
            (fixXRot) ? oriRot.x : transform.eulerAngles.x,
            (fixYRot) ? oriRot.y : transform.eulerAngles.y,
            (fixZRot) ? oriRot.z : transform.eulerAngles.z
            );

        // transform.rotation = Quaternion.Euler(new Vector3(0f, -1f, 0f));
        // transform.rotation = Quaternion.Euler(new Vector3(90f, transform.eulerAngles.y, 0f));
    }
}
