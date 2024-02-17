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
        // EnemyCounter ��ũ��Ʈ�� ����
        EnemyCounter enemyCounter = FindObjectOfType<EnemyCounter>();

        // EnemyCounter ��ũ��Ʈ�� OnEnemyCountChange �̺�Ʈ�� �̺�Ʈ �ڵ鷯 ���
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
        EnemyCounter enemyCounter = FindObjectOfType<EnemyCounter>();
        if (enemyCounter != null)
        {
            enemyCounter.OnEnemyCountChange -= HandleEnemyCountChange;
        }
    }

    // Enemy�� ���� ����� �� ȣ��Ǵ� �Լ�
    void HandleEnemyCountChange(int newEnemyCount)
    {
        if(doorColliders != null && doorColliders.Length > 0 && newEnemyCount == 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i].isTrigger = false;
            }
        }
        else if(doorColliders != null && doorColliders.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i].isTrigger = true;
            }
        }
    }
}
