using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이어가 발사하는 눈물(공격) 오브젝트를 미리 생성해 두는 오브젝트 풀링 스크립트
public class Player_ObjectPooling : MonoBehaviour
{
    public static Player_ObjectPooling instance;
    public GameObject bulletPrefab;

    int bulletAmount = 20;
    Queue<GameObject> bulletQueue = new Queue<GameObject>();
    void Start()
    {
        instance = this;

        for (int index = 0; index < bulletAmount; index++)
        {
            GameObject poolBullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);

            bulletQueue.Enqueue(poolBullet);
            poolBullet.SetActive(false);
        }
    }
    public GameObject GetBulletPool()
    {
        GameObject outObject = bulletQueue.Dequeue();
        outObject.SetActive(true);
        return outObject;
    }
    public void ReturnBulletPool(GameObject insertObject)
    {
        bulletQueue.Enqueue(insertObject);
        insertObject.SetActive(false);
    }
}