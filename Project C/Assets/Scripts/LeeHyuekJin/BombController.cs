using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explosionRadius;
    public float detonationTime;
    public GameObject explosionEffect;
    void Start()
    {
        Invoke("Detonate", detonationTime);
    }

    void Update()
    {
        if(explosionEffect == null)
        {
            Destroy(gameObject);
        }
    }

    private void  Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Obstacle _obstacle = collider.GetComponent<Obstacle>();
                _obstacle.Destroyed();
            }
            if(collider.CompareTag("Enemy"))
            {
                EnemyHealth _EnemyHp = collider.GetComponent<EnemyHealth>();
                _EnemyHp.TakeDamage(10);
            }
            if(collider.CompareTag("Player"))
            {
                IsaacController _playerHp = collider.GetComponent<IsaacController>();
                _playerHp.GetHit();
            }
        }
        Collider2D _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        AudioSource audio;
        audio = GetComponent<AudioSource>();
        audio.Play();
        StartCoroutine(Explosion());

    }

    IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(explosionEffect);
    }
}
