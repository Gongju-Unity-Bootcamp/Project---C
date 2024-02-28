using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakFlyController : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        Debug.Log(transform.position);
    }
}
