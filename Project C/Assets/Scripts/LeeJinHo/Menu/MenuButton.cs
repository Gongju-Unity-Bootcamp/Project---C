using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Ŭ��");
        if (gameObject.name.Equals("StartButton"))
        {
            SceneManager.LoadScene(1);

            
        }

        if (gameObject.name.Equals("SettingButton"))
        {
            //���� �޴� �ҷ����� �޼ҵ�
        }

        if(gameObject.name.Equals("EndButton"))
        {
            Application.Quit();
        }
    }
}
