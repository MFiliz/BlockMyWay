using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private int reachedLevel;
    public Button[] buttonList;
    // Use this for initialization
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("reachedLevel"))
        {
            reachedLevel = PlayerPrefs.GetInt("reachedLevel");
            for (int i = 0; i < buttonList.Length; i++)
            {
                if (reachedLevel >= i)
                {
                    buttonList[i].interactable = true;
                }
            }
        }

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
