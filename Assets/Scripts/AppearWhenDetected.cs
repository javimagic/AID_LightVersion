using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhenDetected : MonoBehaviour
{
    public GameObject victimReckon;
    public GameObject appearingObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (victimReckon.GetComponent<VictimRecognition>().victimDetected) {
            appearingObject.SetActive(true);
        }
    }
}
