using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameOver gameOver;
    public GameObject targetToDestroy;
    public HelicopterController heliController;
    public GameObject followCamera;
    public GameObject explosionCamera;
    public GameObject fire;
    public HasExplodedFlag flag;
    public float explosionSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        flag.hasExploded = false;
    }
    

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Drone" && other.tag != "VictimArea" && other.tag != "VictimReckon" && other.tag != "Player")
        {
            // Destroy(targetToDestroy);
            heliController.helicopterIsAlive = false;
            // followCamera.GetComponent<FollowTargetCamera>().enabled = false;
            // explosionCamera.GetComponent<ExplosionCamera>().enabled = false;
            // GameObject instantiatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
            // instantiatedExplosion.transform.localScale = new Vector3(1f, 1f, 1f) * explosionSize;
            fire.SetActive(true);
            flag.hasExploded = true;
            gameOver.gameOverText = "Vehículo destruido";
            gameOver.enabled = true;
        }
    }
}
