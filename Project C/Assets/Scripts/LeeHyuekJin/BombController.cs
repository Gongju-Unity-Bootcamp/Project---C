using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explosionRadius;
    public float detonationTime;
    public GameObject explosionEffect;
    private GameObject _player;
    private Player_Health _player_Health;
    private Obstacle _obstacle;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        Invoke("Detonate", detonationTime);
        _player = GameObject.FindWithTag("Player");
        _player_Health = _player.GetComponent<Player_Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                _obstacle = collider.GetComponent<Obstacle>();
                _obstacle.Destroyed();
            }
            if(collider.CompareTag("Enemy"))
            {

            }
            if(collider.CompareTag("Player"))
            {
                _player_Health.TakeDamage();
            }
        }
        Collider2D _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        AudioSource audio;
        audio = GetComponent<AudioSource>();
        //audio.Play();
        StartCoroutine(Explosion());

    }

    IEnumerator Explosion()
    {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(explosionEffect);
    }
}
