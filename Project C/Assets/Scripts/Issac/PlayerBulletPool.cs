using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletPool : MonoBehaviour
{
    public static PlayerBulletPool instance;

    public bool collectionChecks = true;
    public int defaultSize = 20;
    public int maxPoolSize = 40;
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
            PlayerBulletController bullet = CreatePooledItem().GetComponent<PlayerBulletController>();
            bullet.Pool.Release(bullet.gameObject);
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        GameObject bullets = Instantiate(bulletPrefab);
        bullets.GetComponent<PlayerBulletController>().Pool = this.Pool;
        return bullets;
    }

    // ���
    private void OnTakeFromPool(GameObject bullets)
    {
        Debug.Log("���");
        bullets.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject bullets)
    {
        Debug.Log("��ȯ");
        bullets.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject bullets)
    {
        Destroy(bullets);
    }
}