using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject boosSpawn;
    public GameObject nextStage;
    public static int enemyCount { get; set; }
    public event Action<int> OnEnemyCountChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        OnEnemyCountChange?.Invoke(enemyCount);

        if(enemyCount == 0 )
        {
            BossRoomClear();
        }
    }

    private void BossRoomClear()
    {
        Instantiate(nextStage,transform.position - new Vector3(0f,4f,0f), Quaternion.identity);
        Debug.Log("아이템 생성");
    }
}
