using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Rigidbody2D prb;
    private Rigidbody2D rb;
    private GameObject player;

    public int hp;
    public float knockbackForce;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        prb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(10);
        }
        else if (collision.gameObject.CompareTag("Player") && Player_Move.hp > 1)
        {
            Debug.Log("플레이어 충돌");
            Knockback(collision.transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Knockback(Vector3 playerPosition)
    {
        Vector2 knockbackDirection = (transform.position - playerPosition).normalized;
    
        StartCoroutine(BossStop());
        prb.AddForce(knockbackDirection * -knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator BossStop()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        yield return null;
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }
}
