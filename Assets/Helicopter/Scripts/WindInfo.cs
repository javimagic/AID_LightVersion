using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindInfo : MonoBehaviour
{
    public float meanWindForce = 3f;
    public Vector3 meanWindDir = new Vector3(1f, 0f, 0f);
    public float windForce;
    public Vector3 windDir;
    public float forceNoise;
    public float dirNoise;

    // Start is called before the first frame update
    void Start()
    {
        meanWindDir = Vector3.Normalize(meanWindDir);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Random.Range(0f, 1f) < 0.001)
        {
            meanWindDir = Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        }
        windDir = meanWindDir + Vector3.Normalize(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))) * dirNoise;
        windForce = meanWindForce + Random.Range(-1f, 1f) * forceNoise;
    }
}
