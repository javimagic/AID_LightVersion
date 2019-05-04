using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Light ambientLight;
    public bool gamePaused = false;
    public Button playBtn;
    private bool canChange = false;
    private bool counterReached = false;
    private bool stoppedPressing = false;
    public GameObject menu;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        counterReached = (counter++ > 5) ? true : false;
        if (counterReached && !stoppedPressing)
        {
            stoppedPressing = !Input.GetButton("PS4_option");
        }
        canChange = stoppedPressing && counterReached;
        if (Input.GetButton("PS4_option") && canChange) {
            if (gamePaused) {
                // Debug.Log("Unpaused");
                unpauseGame();
            }
            else {
                // Debug.Log("Paused");
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        ambientLight.intensity = 0f;
        Time.timeScale = 0f;
        menu.SetActive(true);
        gamePaused = true;
        playBtn.Select();
        // Reset intern values:
        canChange = false;
        counter = 0;
        counterReached = false;
        stoppedPressing = false;
    }

    public void unpauseGame()
    {
        ambientLight.intensity = 1f;
        Time.timeScale = 1.0f;
        menu.SetActive(false);
        gamePaused = false;
        // Reset intern values:
        canChange = false;
        counter = 0;
        counterReached = false;
        stoppedPressing = false;
    }
}
