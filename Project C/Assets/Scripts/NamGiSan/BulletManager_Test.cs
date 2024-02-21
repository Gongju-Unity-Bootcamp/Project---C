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

        // ���� ������Ʈ ����
        for (int i = 0; i < defaultCapacity; i++)
        {
            EnemyBullet bullet = CreatePooledItem().GetComponent<EnemyBullet>();
            //bullet.Pool.Release(bullet.gameObject);
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(bulletPrefab);
        //poolGo.GetComponent<EnemyBullet>().Pool = this.Pool;
        return poolGo;
    }

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

}
