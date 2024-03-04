using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("클릭");
        if (gameObject.name.Equals("StartButton"))
        {
            SceneManager.LoadScene(1);

            
        }

        if (gameObject.name.Equals("SettingButton"))
        {
            //세팅 메뉴 불러오는 메소드
        }

        if(gameObject.name.Equals("EndButton"))
        {
            Application.Quit();
        }
    }
}
