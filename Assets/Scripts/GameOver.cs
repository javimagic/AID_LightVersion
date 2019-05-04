using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string gameOverText = "Misión fallida";
    public Light ambientLight;
    public GameObject gameOverElement;
    public float timer = 3f;

    private float counter;


    // Start is called before the first frame update
    void Start()
    {
        counter = timer;
        gameOverElement.SetActive(true);
        gameOverElement.transform.Find("Text").gameObject.GetComponent<Text>().text = gameOverText;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter -= Time.fixedDeltaTime;
        ambientLight.intensity = Mathf.Lerp(ambientLight.intensity, 0, Time.fixedDeltaTime);
        if (counter <= 0f) SceneManager.LoadScene("AIDS_Menu");
        Debug.Log(counter);
    }
}
