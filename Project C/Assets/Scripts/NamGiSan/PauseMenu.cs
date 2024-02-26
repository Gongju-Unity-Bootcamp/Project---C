using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public GameObject Option;
    public Button fullScreenOnBtn;
    public Button fullScreenOffBtn;

    private bool pauseActive;
    private bool optionActive;
    private bool checkfull;

    private void Awake()
    {
        pauseActive = false;
        optionActive = false;
        checkfull = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (pauseActive == false && optionActive == false)
            {
                OnPause();
            }

            else if (pauseActive == true && optionActive == false)
            {
                OffPause();
            }

            else if (pauseActive == true && optionActive == true)
            {
                OffOption();
            }
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        Pause.SetActive(true);
        pauseActive = true;
    }

    public void OffPause()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
        pauseActive = false;
    }

    public void OnOption()
    {
        Option.SetActive(true);
        optionActive = true;
    }

    public void OffOption()
    {
        Option.SetActive(false);
        optionActive = false;
    }

    public void CheckContinue()
    {
        if (pauseActive == true)
        {
            Time.timeScale = 1;
            pauseActive = false;
            Pause.SetActive(false);
        }
    }

    public void CheckQuit()
    {
        if (pauseActive == true)
        {
            pauseActive = false;
            SceneManager.LoadScene(0);  // 메인메뉴로 이동
        }
    }

    public void OnFullScreen()
    {
        checkfull = true;

        Screen.SetResolution(1920, 1080, true);
        Debug.Log("풀스크린ON");
    }

    public void OffFullScreen()
    {
        checkfull = false;

        Screen.SetResolution(1920, 1080, false);
        Debug.Log("풀스크린OFF");
    }
}
