using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string gameOverTitle = "GAME OVER";
    public string gameOverText = "Misión fallida";

    public Light ambientLight;
    public GameObject gameOverElement;
    public float timer = 3f;
    public bool missionAccomplished = false;

    private float counter;


    // Start is called before the first frame update
    void Start()
    {
        counter = timer;
        gameOverElement.SetActive(true);
        gameOverElement.transform.Find("Title").gameObject.GetComponent<Text>().text = (missionAccomplished)? "CONGRATULATIONS" : "GAME OVER";
        gameOverElement.transform.Find("Title").gameObject.GetComponent<Text>().fontSize = (missionAccomplished) ? 70 : 110;
        gameOverElement.transform.Find("Text").gameObject.GetComponent<Text>().text = gameOverText;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter -= Time.fixedDeltaTime;
        ambientLight.intensity = Mathf.Lerp(ambientLight.intensity, 0, Time.fixedDeltaTime);
        if (counter <= 0f) SceneManager.LoadScene("AIDS_Menu");
    }
}
