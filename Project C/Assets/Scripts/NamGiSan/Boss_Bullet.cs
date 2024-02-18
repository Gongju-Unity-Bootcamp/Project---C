using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public float damage = 1.0f;

    void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterRange());
    }

    IEnumerator ReturnBulletAfterRange()
    {
        float ranTime = Random.Range(0.7f, 1.0f);

        yield return new WaitForSeconds(ranTime);
        ResetBossBullet();
    }

    public void ResetBossBullet()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        GetComponent<Collider2D>().enabled = false;
        Boss_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        Boss_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
}
