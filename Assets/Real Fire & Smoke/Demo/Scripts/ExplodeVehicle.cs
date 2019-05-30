using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeVehicle : MonoBehaviour
{
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(effect, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
