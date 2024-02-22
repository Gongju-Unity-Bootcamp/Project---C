using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoxCollider2D m_wallCollider;
    private Rigidbody2D m_body2D;
    private bool isDoor = true;
    float m_time = 0;
    private void Awake()
    {
        m_wallCollider = GetComponent<BoxCollider2D>();
        m_body2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }

    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}