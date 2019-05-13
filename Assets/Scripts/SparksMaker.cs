using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksMaker : MonoBehaviour
{
    public GameObject spark;
    private Transform contactTransform;
    private GameObject singleSpark;

    // Start is called before the first frame update
    void Start()
    {
        contactTransform = transform;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        // contactTransform.position = collision.GetContact(1).point;
        // contactTransform.rotation = Quaternion.FromToRotation(Vector3.up, collision.GetContact(0).normal);
        foreach (ContactPoint contact in collision.contacts) {
            contactTransform.position = contact.point;
            contactTransform.rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            singleSpark = Instantiate(spark, contactTransform);
            Destroy(singleSpark, 0.1f);
        }
    }

}
