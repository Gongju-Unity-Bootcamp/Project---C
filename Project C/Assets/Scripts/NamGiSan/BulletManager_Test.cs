using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager_Test : MonoBehaviour
{
    public static BulletManager_Test instance;

    public int defaultCapacity = 25;
    public int maxPoolSize = 50;
    public GameObject bulletPrefab;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        // 사전 오브젝트 생성
        for (int i = 0; i < defaultCapacity; i++)
        {
            EnemyBullet bullet = CreatePooledItem().GetComponent<EnemyBullet>();
            //bullet.Pool.Release(bullet.gameObject);
        }
    }

    // 생성
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(bulletPrefab);
        //poolGo.GetComponent<EnemyBullet>().Pool = this.Pool;
        return poolGo;
    }

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

}
