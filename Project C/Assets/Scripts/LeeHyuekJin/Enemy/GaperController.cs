using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaperController : MonoBehaviour
{
    private AIPath _aipath;
    private float detectionRange = 8f;
    private GameObject player;
    private void Start()
    {
        _aipath = GetComponentInParent<AIPath>();
        _aipath.canMove = false;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < detectionRange)
        {
            _aipath.canMove = true;
        }
    }
    private void OnDestroy()
    {
        // 부모 오브젝트 파괴
        Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("충돌");
            _aipath.canMove = true;
        }
    }
}