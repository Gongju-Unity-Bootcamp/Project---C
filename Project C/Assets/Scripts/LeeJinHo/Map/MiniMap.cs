using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public MiniMap miniMap;
    [SerializeField] private GameObject[] m_miniMaps;
    //[SerializeField] private GameObject m_miniCamera;
    private Vector3 m_testposition;


    void Awake()
    {
        m_miniMaps = new GameObject[transform.childCount];
        transform.position = new Vector3(300, 300, 0);
        Init();
    }


    private int m_Count = 0;
    void Init()
    {
        foreach (Transform tile in transform)
        {
            Transform m_mini = transform.Find($"MiniMap ({m_Count})");

            if (m_mini == null) { continue; }

            m_miniMaps[m_Count] = m_mini.gameObject;
            m_Count += 1;
        }
    }

    private int m_count = 0;
    public void MiniMapSetting(Vector3 deltaPosition)
    {

        if (m_testposition == Vector3.zero)
        {
            m_testposition = transform.position;
        }
        else
        {
            m_testposition += deltaPosition;
        }


        m_miniMaps[m_count].transform.position = m_testposition;

        m_count += 1;
    }
}
