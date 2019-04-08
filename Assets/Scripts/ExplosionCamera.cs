using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCamera : MonoBehaviour
{
    public GameObject target;
    public float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.transform.position + Vector3.up * distance;
    }
}
