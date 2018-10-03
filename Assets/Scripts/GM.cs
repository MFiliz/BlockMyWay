using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Text score;
    private int scoreCount = 0;
    // Use this for initialization
    void Start()
    {
        score.text = "Skor : " + scoreCount;
        Debug.Log(PlayerPrefs.GetString("reachedLevel"));
    }

    // Update is called once per frame
    void Update()
    {
        if (YeniMovement.IsLevelInstantiated)
        {
            GameObject gameControllerObject = GameObject.FindWithTag("enemy");
            if (gameControllerObject == null)
            {
                score.text = "Oyun Bitti";
                if (PlayerPrefs.GetInt("reachedLevel") <= System.Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Level_", "")))
                {
                    PlayerPrefs.SetInt("reachedLevel", System.Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Level_", "")));
                }

            }
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            //Debug.Log("Düştüm");
            SkoruArttir();
        }
        if (other.tag == "Player")
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        //Debug.Log(other.name);

    }



    private void SkoruArttir()
    {
        scoreCount++;
        score.text = "Skor : " + scoreCount.ToString();
    }

}
