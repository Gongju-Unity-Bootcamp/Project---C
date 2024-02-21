using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject blood;
    private void Start()
    {
        UpdateEnemyCount();
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            if (blood != null)
            {
                Instantiate(blood, transform.position, Quaternion.identity);
            }
            UpdateEnemyCount();
        }
    }

    private void UpdateEnemyCount()
    {
        RoomManager.Instance.UpdateEnemyCount();
    }
}
