using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseActive;
    public GameObject MenuSet;

    private void Awake()
    {
        pauseActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            CheckPause();
        }
    }

    public void CheckPause()
    {
        if (Player_Move.gameState == "playing" && pauseActive == false)
        {
            Time.timeScale = 0;
            MenuSet.SetActive(true);
            pauseActive = true;
        }
        else
        {
            Time.timeScale = 1;
            MenuSet.SetActive(false);
            pauseActive = false;
        }
    }

    public void CheckContinue()
    {
        if (Player_Move.gameState == "playing" && pauseActive == true)
        {
            Debug.Log("클릭");
            Time.timeScale = 1;
            pauseActive = false;
            MenuSet.SetActive(false);
        }    
    }

    public void CheckQuit()
    {
        if (Player_Move.gameState == "playing" && pauseActive == true)
        {
            Debug.Log("클릭");
            pauseActive = false;
            SceneManager.LoadScene(0);
        }
    }
}
