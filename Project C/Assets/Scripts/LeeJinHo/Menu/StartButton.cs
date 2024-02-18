using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject m_cameraMove;



    public void OnClick()
    {
        m_cameraMove.SetActive(true);
    }
}
