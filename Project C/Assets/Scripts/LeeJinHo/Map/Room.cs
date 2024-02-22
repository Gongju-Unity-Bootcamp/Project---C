using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Room : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;
    [SerializeField] private GameObject m_Camera;

    public Color redColor = Color.red;
    public Color whiteColor = Color.white;
    private Renderer rend;


    void Start()
    {
        rend = transform.Find("MiniMap").GetComponent<Renderer>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            m_Camera.transform.position = transform.position + new Vector3(0, 0, -10);

            if (rend != null)
            {
                rend.material.color = redColor;
            }
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && rend != null)
        {
            rend.material.color = whiteColor;
        }
    }
}
