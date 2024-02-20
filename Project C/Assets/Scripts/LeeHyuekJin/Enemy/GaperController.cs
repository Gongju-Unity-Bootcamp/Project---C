using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GaperController : MonoBehaviour
{
    private float detectionRange = 3f;
    private GameObject player;
    private Animator _animator;
    private bool OnAttak = false;
    public float moveSpeed;
    private Rigidbody2D _rb;
    private Vector2 direction;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (!OnAttak && distanceToPlayer < detectionRange)
        {
            Move();
            _animator.SetTrigger("OnHit");
            OnAttak = true;
        }
        else if(OnAttak)
        {
            Move();
            if (_rb.velocity.x > 0)
            {
                _animator.SetTrigger("MoveRight");
            }
            else if (_rb.velocity.x < 0)
            {
                _animator.SetTrigger("MoveLeft");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!OnAttak && collision.gameObject.CompareTag("PlayerBullet"))
        {
            OnAttak = true;
        }
    }
    private void Move()
    {
        direction = player.transform.position - transform.position;
        _rb.velocity = direction.normalized * moveSpeed;
    }
}