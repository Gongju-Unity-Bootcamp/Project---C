using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    public GameObject[] bulletPrefab;

    int bulletAmount = 35;
    Queue<GameObject> bulletQueue = new Queue<GameObject>();

    void Start()
    {
        instance = this;

        for (int index = 0; index < bulletAmount; index++)
        {
            GameObject poolBullet = Instantiate(bulletPrefab[0], Vector3.zero, Quaternion.identity);

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
