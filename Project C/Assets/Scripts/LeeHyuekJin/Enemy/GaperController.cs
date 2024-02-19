using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaperController : MonoBehaviour
{
    private AIPath _aipath;
    private float detectionRange = 4f;
    private GameObject player;
    private Animator _animator;
    private bool OnAttak = true;
    private void Start()
    {
        _aipath = GetComponentInParent<AIPath>();
        _aipath.canMove = false;
        player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (OnAttak && distanceToPlayer < detectionRange)
        {
            _animator.SetTrigger("OnDetectedPlayer");
            _animator.SetBool("OnAttakState", true);
            _aipath.canMove = true;
            OnAttak = false;
        }
    }
    private void OnDestroy()
    {
        // 부모 오브젝트 파괴
        Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnAttak && collision.gameObject.CompareTag("PlayerBullet"))
        {
            _animator.SetBool("OnAttakState", true);
            _aipath.canMove = true;
            OnAttak = false;
        }
    }
}