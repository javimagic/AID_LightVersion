using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEulerAngles : MonoBehaviour {
    public Rigidbody obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xAng, zAng;
        xAng = obj.transform.localEulerAngles.x;
        if (xAng > 180) xAng -= 360;
        xAng *= -1;
        zAng = obj.transform.localEulerAngles.z;
        if (zAng > 180) zAng -= 360;
        zAng *= -1;
        Debug.Log("x = " + xAng + "; z = " + zAng + ";");
    }
}
