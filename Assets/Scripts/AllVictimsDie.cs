using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllVictimsDie : MonoBehaviour
{
    public GameOver gameOver;
    public List<GameObject> victims;

    private bool allVictimsAreDead = false;
    // Start is called before the first frame update

    private void Start() {
        allVictimsAreDead = false;
    }

    void gameIsOver()
    {
        gameOver.gameOverText = "Víctimas fallecidas";
        gameOver.enabled = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        allVictimsAreDead = true;
        foreach (GameObject victim in victims) {
            if (victim.GetComponent<CountDown>().victimIsAlive) allVictimsAreDead = false;
        }
        if (allVictimsAreDead) gameIsOver();
    }
}
