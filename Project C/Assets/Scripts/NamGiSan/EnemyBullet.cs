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
        BossHealth boss = FindObjectOfType<BossHealth>();

        if(boss != null)
        {
            float ranTime = Random.Range(0.3f, 1.5f);
            yield return new WaitForSeconds(ranTime);
            StartCoroutine(ResetBossBullet());
        }
    }

    IEnumerator ResetBossBullet()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("pop");
        yield return new WaitForSeconds(0.5f);
        BulletManager.instance.ReturnBulletPool(gameObject);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||!collision.gameObject.CompareTag("PlayerBullet") || !collision.gameObject.CompareTag("EnemyBullet")
            || collision.gameObject.CompareTag("Box"))
        {
            StartCoroutine(ResetBossBullet());
        }
    }
}
