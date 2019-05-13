using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoundary : MonoBehaviour
{
    // public CountDown victim1;
    // public CountDown victim2;
    // public CountDown victim3;
    public GameOver gameOver;
    public List<GameObject> victims;
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
        bool allVictimsAboard = true;
        if (other.tag == "Player" && helicopterController.IsOnGround) //  && victim1.victimAboard && victim2.victimAboard && victim3.victimAboard 
        {
            foreach (GameObject victim in victims)
            {
                if (victim.activeSelf && !victim.GetComponent<CountDown>().victimAboard) allVictimsAboard = false;
            }
            if (allVictimsAboard) {
                gameOver.missionAccomplished = true;
                gameOver.gameOverText = "Mission accomplished";
                gameOver.enabled = true;
                foreach (GameObject victim in victims) victim.GetComponent<CountDown>().stopRunning();
            }
        }
    }

}
