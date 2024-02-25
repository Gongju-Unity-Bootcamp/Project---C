using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    public float knockbackForce;
    private Animator _animator;
    private GameObject _player;
    private PlayerStats _playerStats;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();
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
            TakeDamage(_playerStats.attackDamage);
            _animator.SetTrigger("OnHit");
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            _animator.SetTrigger("Dead");
            Invoke("Dead", 0.3f);
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
    private void Dead()
    {
        Destroy(gameObject);
    }
}
