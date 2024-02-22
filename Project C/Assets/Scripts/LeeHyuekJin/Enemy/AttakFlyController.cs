using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakFlyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameObject player;
    private Vector2 direction;
    public float moveSpeed;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Move();   
    }

    private void Move()
    {
        direction = player.transform.position - transform.position;
        _rb.velocity = direction.normalized * moveSpeed;
    }
}
