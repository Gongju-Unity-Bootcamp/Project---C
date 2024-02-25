using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Rigidbody2D _prb;
    private Rigidbody2D _rb;
    private Collider2D _col;
    private Animator _animator;

    public float hp;
    public float knockbackForce;

    private GameObject _player;
    private PlayerStats _playerStats;
    private IsaacController isaac;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();
        isaac = GetComponent<IsaacController>();

        _prb = _player.GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(_playerStats.attackDamage);
            _animator.SetTrigger("OnHit");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌");
            Knockback(collision.transform.position);
        }
    }

    private void Knockback(Vector3 playerPosition)
    {
        Vector2 knockbackDirection = (transform.position - playerPosition).normalized;

        StartCoroutine(BossStop());
        _prb.AddForce(knockbackDirection * -knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator BossStop()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        yield return null;
        _rb.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;


        if (hp <= 0)
        {
            StartCoroutine(BossStop());
            _col.enabled = false;
            _animator.SetTrigger("Dead");

            StartCoroutine(BloodEffect());  

            Invoke("Dead", 2.1f);
        }
    }

    IEnumerator BloodEffect()
    {
        GameObject bloodBag = transform.GetChild(2).gameObject;

        for (int i = 0; i < 17; i++)
        {
            GameObject bloodEffect = bloodBag.transform.GetChild(i).gameObject;
            bloodEffect.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }  


}
