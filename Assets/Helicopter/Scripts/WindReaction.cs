using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReaction : MonoBehaviour
{
    public WindInfo globalWind;
    public float windOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit positHit;
        Ray positWindRay = new Ray(transform.position, globalWind.windDir);
        Physics.Raycast(positWindRay, out positHit);

        Vector3 heliPos = GameObject.Find("Player").transform.position;
        GetComponent<Rigidbody>().AddForceAtPosition(globalWind.windDir * globalWind.windForce, transform.position + transform.forward * windOffset);
    }
}
