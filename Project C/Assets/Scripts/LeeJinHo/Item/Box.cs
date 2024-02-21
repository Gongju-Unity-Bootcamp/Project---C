using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_boxRb;
    [SerializeField] private BoxCollider2D m_boxCol;
    [SerializeField] private GameObject m_openTimeLine;

    [SerializeField] private Animator m_open;
    [SerializeField] private Animator m_close;

    [SerializeField] private bool m_isCheck;

    [SerializeField] private List<GameObject> items;
    [SerializeField] private Transform itemDropPoint;

    void Start()
    {
        m_boxRb = GetComponent<Rigidbody2D>();
        m_boxCol = GetComponent<BoxCollider2D>();
        m_openTimeLine = transform.Find("BoxOpen").gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_openTimeLine.SetActive(true);
            BoxOpen();
        }
    }

    void BoxOpen()
    {
        if (!m_isCheck) 
        {
            m_close.SetBool("Open", true);
            m_isCheck = true;
        }

        if (m_isCheck)
        {
            m_open.Play("BoxOpen");
            DropItem();
        }
    }
    void DropItem()
    {
        int randomIndex = Random.Range(0, items.Count);
        GameObject randomItem = items[randomIndex];
        Instantiate(randomItem, itemDropPoint.position, Quaternion.identity);
    }
}
