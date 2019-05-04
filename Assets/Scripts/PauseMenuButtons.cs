using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Button resumeBtn;
    public GameObject areYouSurePanel;
    private bool exitPressed;
    private Button noBtn;

    // Start is called before the first frame update
    void Start()
    {
        noBtn = areYouSurePanel.transform.Find("Btn_No").gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resumeBtnClicked ()
    {
        pauseMenu.unpauseGame();
    }

    public void mainMenuBtnClicked()
    {
        exitPressed = false;
        areYouSurePanel.SetActive(true);
        noBtn.Select();
    }

    public void exitBtnClicked () {
        exitPressed = true;
        areYouSurePanel.SetActive(true);
        noBtn.Select();
    }

    public void iAmSureClicked() {
        if (exitPressed) Application.Quit();
        else {
            pauseMenu.unpauseGame();
            SceneManager.LoadScene("AIDS_Menu");
        }
    }

    public void iAmNotSureClicked()
    {
        resumeBtn.Select();
        areYouSurePanel.SetActive(false);
    }
}
