using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ObjectPooling : MonoBehaviour
{
    public static Player_ObjectPooling instance;
    public GameObject bulletPrefab;

    int bulletAmount = 5;
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
