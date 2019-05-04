using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDroneCamera : MonoBehaviour
{
    public Transform Target;
    public float PositionFolowForce = 15f;
    public float horizontalOffset = 2f;
    public float verticalOffset = 0.75f;
    public GameObject VictimReconCol;
    public ControlChanger changer;


    void Start()
    {

    }

    void FixedUpdate()
    {
        var vector = Vector3.forward;
        var dir = Target.rotation * Vector3.forward;
        Vector3 goalPosition;
        dir.y = 0f;
        if (dir.magnitude > 0f) vector = dir / dir.magnitude;

        goalPosition = Target.position - horizontalOffset * Vector3.Normalize(Vector3.ProjectOnPlane(Target.forward, Vector3.up)) +
            Vector3.up * verticalOffset;
        
        transform.position = Vector3.Lerp(transform.position, goalPosition, PositionFolowForce * Time.deltaTime);
        transform.LookAt(Target);

        // -------- Victim Recognition Collider readjustment --------------

        if (!changer.controllingHeli) {
            Quaternion colliderOrientation = Quaternion.LookRotation(goalPosition - transform.position);
            VictimReconCol.transform.rotation = transform.rotation;
        }
    }
}