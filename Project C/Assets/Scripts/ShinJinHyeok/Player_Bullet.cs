using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    float range = 1.0f;
    void FixedUpdate()
    {
        Invoke("ReturnBullet", range);
    }
    void ReturnBullet()
    {
        range = 1.0f;
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
}