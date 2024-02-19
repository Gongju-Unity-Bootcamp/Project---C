using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    // �̱��� ������ ����Ͽ� Instance ����
    public static EnemyCounter Instance { get; private set; }

    // �̺�Ʈ ����
    public event Action<int> OnEnemyCountChange;

    // Enemy ���� �����ϴ� ����
    public static int enemyCount { get; set; }

    private void Awake()
    {
        // Instance �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        enemyCount = 0;
        UpdateEnemyCount();
    }

    private void OnDestroy()
    {
        OnEnemyCountChange = null;
    }

    public void UpdateEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        Debug.Log(enemies.Length);
        OnEnemyCountChange?.Invoke(enemyCount);
    }

}
