using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopController : MonoBehaviour
{
    public Sprite[] poopSprite;
    private SpriteRenderer _spriteRenderer;
    private int hitCount = 0;
    private Collider2D _collider2D;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") && hitCount < poopSprite.Length)
        {
            _spriteRenderer.sprite = poopSprite[hitCount];
            hitCount++;
            if (hitCount == poopSprite.Length)
            {
                _audioSource.Play();
                OffCollider();
            }
        }
    }

    private void OffCollider()
    {
        _collider2D.enabled = false;
    }

}
