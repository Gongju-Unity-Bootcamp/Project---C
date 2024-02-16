using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    float range = 1.2f;
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
        range = 1.0f;
        ResetBullet();
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
    public void ResetBullet()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
}