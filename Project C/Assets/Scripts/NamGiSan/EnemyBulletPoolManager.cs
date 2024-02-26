using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPoolManager : MonoBehaviour
{
    public static EnemyBulletPoolManager instance;

    public bool collectionChecks = true;
    public int defaultSize = 35;
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
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultSize, maxPoolSize);

        // �̸� ������Ʈ ���� �س���
        for (int i = 0; i < defaultSize; i++)
        {
            EnemyBullet bullet = CreatePooledItem().GetComponent<EnemyBullet>();
            bullet.Pool.Release(bullet.gameObject);
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        GameObject bullets = Instantiate(bulletPrefab);
        bullets.GetComponent<EnemyBullet>().Pool = this.Pool;
        return bullets;
    }

    // ���
    private void OnTakeFromPool(GameObject bullets)
    {
        bullets.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject bullets)
    {
        bullets.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject bullets)
    {
        Destroy(bullets);
    }
}