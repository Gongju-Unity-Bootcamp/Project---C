using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    // Ω∫≈»
    public float attack = 10.0f;
    public static float range = 1.2f;
    void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterRange());
    }
    IEnumerator ReturnBulletAfterRange()
    {
        yield return new WaitForSeconds(range);
        ReturnBullet();
    }
    void ReturnBullet()
    {
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
}