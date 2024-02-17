using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    // 싱글톤 패턴을 사용하여 Instance 생성
    public static EnemyCounter Instance { get; private set; }

    // 이벤트 선언
    public event Action<int> OnEnemyCountChange;

    // Enemy 수를 추적하는 변수
    public static int enemyCount { get; set; }

    private void Awake()
    {
        // Instance 초기화
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

    public void UpdateEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        OnEnemyCountChange?.Invoke(enemyCount);
    }
}
