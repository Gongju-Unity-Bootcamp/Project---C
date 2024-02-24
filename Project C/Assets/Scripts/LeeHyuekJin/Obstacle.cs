using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Sprite dreg;

    private SpriteRenderer _spriteRenderer;
    Collider2D _collider2D;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void Destroyed()
    {
        _spriteRenderer.sprite = dreg;

        _collider2D.enabled = false;
    }
}
