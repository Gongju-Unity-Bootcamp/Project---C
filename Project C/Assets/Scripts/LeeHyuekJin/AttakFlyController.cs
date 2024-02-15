using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakFlyController : MonoBehaviour
{
    private AIPath _aiPath;
    private float originalSpeed;
    private void Start()
    {
        _aiPath = GetComponent<AIPath>();
        originalSpeed = _aiPath.maxSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("CollisionPlayer");
        }
    }

    IEnumerator CollisionPlayer()
    {
        Debug.Log("�ӵ�0");
        _aiPath.maxSpeed = 0;
        yield return new WaitForSeconds(1);
        Debug.Log("�����ӵ�");
        _aiPath.maxSpeed = originalSpeed;
        yield return null;
    }
}
