using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Bam());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Bam()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
