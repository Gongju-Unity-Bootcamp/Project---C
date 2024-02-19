using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject navi;
    private NaviController _naviController;
    void Start()
    {
        navi = GameObject.FindWithTag("GameController");
        _naviController = navi.GetComponent<NaviController>();
    }

    private void OnDestroy()
    {
        _naviController.Scan(0);
    }
}
