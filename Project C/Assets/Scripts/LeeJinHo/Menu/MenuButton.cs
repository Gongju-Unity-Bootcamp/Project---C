using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Ŭ��");
        if (gameObject.name.Equals("StartButton"))
        {
            Debug.Log("��");
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
