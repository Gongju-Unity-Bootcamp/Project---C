using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp;
    public float knockbackForce;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            ApplyKnockback(collision.transform.position);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(100);
            _animator.SetTrigger("OnHit");
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyKnockback(Vector3 playerPosition)
    {
        
        Vector2 knockbackDirection = (transform.position - playerPosition).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb != null )
        {
            Debug.Log(knockbackDirection * knockbackForce);
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

}
