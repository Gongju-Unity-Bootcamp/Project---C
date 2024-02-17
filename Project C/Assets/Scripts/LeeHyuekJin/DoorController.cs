using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject[] doors;
    private Collider2D[] doorColliders;

    void Start()
    {
        StartCoroutine(CountingDoor());
        // EnemyCounter 스크립트를 참조
        EnemyCounter enemyCounter = FindObjectOfType<EnemyCounter>();

        // EnemyCounter 스크립트의 OnEnemyCountChange 이벤트에 이벤트 핸들러 등록
        if (enemyCounter != null)
        {
            enemyCounter.OnEnemyCountChange += HandleEnemyCountChange;
        }

        HandleEnemyCountChange(EnemyCounter.enemyCount);
    }

    IEnumerator CountingDoor()
    {
        yield return new WaitForEndOfFrame();
        doors = GameObject.FindGameObjectsWithTag("Door");
        doorColliders = new Collider2D[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i] = doors[i].GetComponent<Collider2D>();
        }
        Debug.Log(doorColliders.Length);
    }
    private void OnDestroy()
    {
        // 스크립트가 파괴될 때 이벤트 핸들러 제거
        EnemyCounter enemyCounter = FindObjectOfType<EnemyCounter>();
        if (enemyCounter != null)
        {
            enemyCounter.OnEnemyCountChange -= HandleEnemyCountChange;
        }
    }

    // Enemy의 수가 변경될 때 호출되는 함수
    void HandleEnemyCountChange(int newEnemyCount)
    {
        if(doorColliders != null && newEnemyCount == 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i].isTrigger = false;
            }
        }
        else if(doorColliders != null)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i].isTrigger = true;
            }
        }
    }
}
