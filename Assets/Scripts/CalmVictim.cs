using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CalmVictim : MonoBehaviour
{
    public GameObject victim1;
    public GameObject victim2;
    public GameObject victim3;
    public float maxDistance = 7f;
    private CountDown timer1;
    private CountDown timer2;
    private CountDown timer3;


    // Start is called before the first frame update
    void Start()
    {
        timer1 = victim1.GetComponent<CountDown>();
        timer2 = victim2.GetComponent<CountDown>();
        timer3 = victim3.GetComponent<CountDown>();
    }

    public void calmVictim()
    {
        if (Vector3.Distance(transform.position, victim1.transform.position) <= maxDistance) timer1.victimCalmed();
        if (Vector3.Distance(transform.position, victim2.transform.position) <= maxDistance) timer2.victimCalmed();
        if (Vector3.Distance(transform.position, victim3.transform.position) <= maxDistance) timer3.victimCalmed();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("PS4_X")) calmVictim();
    }
}