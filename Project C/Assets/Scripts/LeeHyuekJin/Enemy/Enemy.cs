using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        UpdateEnemyCount();
    }

    private void OnDestroy()
    {
        UpdateEnemyCount();
    }

    private void UpdateEnemyCount()
    {
        EnemyCounter.Instance.UpdateEnemyCount();
    }
}
