using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public GameObject Option;
    
    private bool pauseActive;
    private bool optionActive;

    private void Awake()
    {
        pauseActive = false;
        optionActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (pauseActive == false && optionActive == false)
            {
                // 일시정지창 On
                CheckPause();
            }

            else if (pauseActive == true && optionActive == false)
            {
                // 일시정지창 Off
            }

            else if (pauseActive == true && optionActive == true)
            {
                // 일시정지창 On
                // 옵션창 Off
            }
        }      
    }

    public void CheckPause()
    {
        if (pauseActive == false)
        {
            Time.timeScale = 0;
            Pause.SetActive(true);
            pauseActive = true;
        }
        else
        {
            Time.timeScale = 1;
            Pause.SetActive(false);
            pauseActive = false;
        }
    }

    public void CheckContinue()
    {
        if (pauseActive == true)
        {

            Debug.Log("클릭");
            Time.timeScale = 1;
            pauseActive = false;
            Pause.SetActive(false);
        }    
    }

    public void CheckQuit()
    {
        if (pauseActive == true)
        {
            Debug.Log("클릭");
            pauseActive = false;
            SceneManager.LoadScene(0);
        }
    }
}
