using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletController : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }

    Rigidbody2D _bulletRbody;
    Animator _bulletAnimator;
    public float attakDamage;
    bool iscoroutine;

    void Start()
    {
        _bulletRbody = GetComponent<Rigidbody2D>();
        _bulletAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!iscoroutine)
        {
            Init();
        }
    }

    void Init()
    {
        StartCoroutine("DestroyBulAnim");
        iscoroutine = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _bulletRbody.gravityScale = 0;
        Managers.Sound.EffectSoundChange("TearImpacts2");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attakDamage);
        }
        
        StartCoroutine(OnColBullet());
    }

    IEnumerator OnColBullet()
    {
        StopCoroutine("DestroyBulAnim");

        Debug.Log("충돌 파괴");
        _bulletRbody.velocity = Vector2.zero;
        _bulletAnimator.SetTrigger("Pop");

        yield return new WaitForSeconds(0.5f);
        iscoroutine = false;
        Pool.Release(gameObject);
    }

    IEnumerator DestroyBulAnim()
    {
        _bulletRbody.gravityScale = 0;
        float lifeTime = Managers.PlayerStats.bulletSurviveTime + 0.5f;

        Debug.Log("사거리 코루틴 진입");
        yield return new WaitForSeconds(lifeTime - 0.6f);
        _bulletRbody.gravityScale = 5;

        yield return new WaitForSeconds(0.1f);

        Managers.Sound.EffectSoundChange("TearImpacts2");

        _bulletRbody.gravityScale = 0;
        _bulletRbody.velocity = Vector2.zero;
        _bulletAnimator.SetTrigger("Pop");

        yield return new WaitForSeconds(0.5f);

        iscoroutine = false;
        Pool.Release(gameObject);
    }
}