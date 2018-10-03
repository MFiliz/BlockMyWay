using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button LevelSelect;
    public Button StartButton;
    public Text StartText;
    private void Awake()
    {
      //  PlayerPrefs.DeleteAll();
    }

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("reachedLevel"))
        {
            StartText.text = "CONTINUE";
            StartButton.onClick.AddListener(delegate { ResumeGame(PlayerPrefs.GetInt("reachedLevel") + 1); });
            LevelSelect.gameObject.SetActive(true);
        }
        else
        {
            StartText.text = "START";
            StartButton.onClick.AddListener(delegate { ResumeGame(1); });
            LevelSelect.gameObject.SetActive(false);
        }


    }

    private void ResumeGame(int v)
    {
        string levelName = "Level_" + v.ToString();
        ChangeScene(levelName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        Color col = new Color(0.1f, 0.05f, 0.3f);
        Initiate.Fade(sceneName, col, 1.0f);

    }
}
