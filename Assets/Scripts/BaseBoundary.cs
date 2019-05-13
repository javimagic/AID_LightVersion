using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoundary : MonoBehaviour
{
    public CountDown victim1;
    public CountDown victim2;
    public CountDown victim3;
    public GameOver gameOver;
    public HelicopterController helicopterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && victim1.victimAboard && victim2.victimAboard && victim3.victimAboard && helicopterController.IsOnGround)
        {
            gameOver.missionAccomplished = true;
            gameOver.gameOverText = "Mission accomplished";
            gameOver.enabled = true;
            victim1.stopRunning();
            victim2.stopRunning();
            victim3.stopRunning();
        }
    }

}
