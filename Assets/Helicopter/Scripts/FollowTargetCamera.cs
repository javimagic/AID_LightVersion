using UnityEngine;

public class FollowTargetCamera : MonoBehaviour
{
    public Transform Target;
    public float PositionFolowForce = 5f;
    public float RotationFolowForce = 5f;
    public float camSensitivity = 300f;
	void Start ()
	{

	}

    void FixedUpdate()
	{
        var vector = Vector3.forward;
        var dir = Target.rotation * Vector3.forward;
		dir.y = 0f;
        if (dir.magnitude > 0f) vector = dir / dir.magnitude;

        transform.position = Vector3.Lerp(transform.position, Target.position, PositionFolowForce * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vector), RotationFolowForce * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(Input.GetAxis("PS4_RightAnalogVert") * camSensitivity, Input.GetAxis("PS4_RightAnalogHoriz") * camSensitivity, 0f), RotationFolowForce * Time.deltaTime);
	}
}

