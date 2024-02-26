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
    private SpriteRenderer _sprite;

    public float hp;
    public float knockbackForce;

    private GameObject _player;
    private PlayerStats _playerStats;
    private Vector2 savePos;
    private float maxHp;
    void Start()
    {
        maxHp = hp;
        _player = GameObject.FindWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();

        _prb = _player.GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            StartCoroutine(TakeDamage(_playerStats.attackDamage));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Knockback(collision.transform.position);
        }
    }

    private void Knockback(Vector3 playerPosition)
    {
        Vector2 knockbackDirection = (transform.position - playerPosition).normalized;

        _prb.AddForce(knockbackDirection * -knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator TakeDamage(float damage)
    {

        hp -= damage;
        savePos = transform.position;
        _sprite.color = new Color(1, 0.2f, 0.2f);
        CheckHp();
        yield return new WaitForSeconds(0.15f);
        _sprite.color = new Color(1, 1, 1);
    }

    private void CheckHp()
    {
        if (hp > 0)
        {
            Managers.UI.UpdateBossHP(hp, maxHp);
            return;
        }

        StartCoroutine(Dead());
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

    IEnumerator Dead()
    {
        transform.position = savePos;
        _col.enabled = false;
        _rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        _animator.SetTrigger("Dead");
        StartCoroutine(BloodEffect());
        yield return new WaitForSeconds(1.75f);
        _sprite.color = new Color(0, 0, 0, 0);    

        Invoke("DistroyBoss", 2.1f);
    }

    private void DistroyBoss()
    {
        Destroy(gameObject);
    }  


}
