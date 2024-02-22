using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private GameObject player;

    public float bulletSpeed = 10f;
    public float damage = 1.0f;

    void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterRange());
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
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
        BulletManager.instance.ReturnBulletPool(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //animator.SetTrigger("Pop");
        BulletManager.instance.ReturnBulletPool(gameObject);
    }

    public void StraightShot()
    {

    }

    public void SpreadShot()
    {

    }

    public void RandomShot()
    {
        
    }
}
