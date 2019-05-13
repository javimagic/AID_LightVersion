using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameOver gameOver;
    public float startTime;
    public bool victimIsAlive = true;
    public bool victimAboard = false;
    public GameObject timer;
    public float timeGainedWhenCalmed;
    private Text timeLabel;
    private float counter;
    private bool shouldCount = true;
    private bool victimIsCalmed = false;

    void Start() {
        counter = startTime;
        timeLabel = timer.transform.Find("Text").gameObject.GetComponent<Text>();
        victimIsCalmed = false;
    }

    public void victimDies() {
        counter = 0f;
        victimIsAlive = false;
        shouldCount = false;
        timer.GetComponent<Image>().color = new Color(1f, 0f, 0f);
    }

    public void victimGoesIn() {
        victimAboard = true;
        timer.GetComponent<Image>().color = new Color(0f, 1f, 0f);
    }

    public void stopRunning() {
        shouldCount = false;
    }

    public void victimCalmed() {
        if (!victimIsCalmed) {
            victimIsCalmed = true;
            counter += timeGainedWhenCalmed;
        }
    }

    void FixedUpdate() {
        if (!shouldCount) return;
        counter -= Time.fixedDeltaTime;
        if (counter <= 0f) victimDies();
        timeLabel.text = counter.ToString("F");
    }
}
