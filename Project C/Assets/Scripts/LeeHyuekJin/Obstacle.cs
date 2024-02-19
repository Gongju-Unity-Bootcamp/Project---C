using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject navi;
    private NaviController _naviController;
    public GameObject dregs;
    void Start()
    {
        navi = GameObject.FindWithTag("GameController");
        _naviController = navi.GetComponent<NaviController>();
    }

    private void OnDestroy()
    {
        if(Application.isPlaying)
        {
            Debug.Log("��ֹ� �ı�");
            _naviController.Scan(0);
            Instantiate(dregs, transform.position, Quaternion.identity);
        }

    }
}
