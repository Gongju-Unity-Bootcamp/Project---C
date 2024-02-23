using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D m_boxRb;
    private BoxCollider2D m_boxCol;
    public ItemType itemType;

    [SerializeField] private GameObject m_openTimeLine;

    [SerializeField] private Animator m_open;
    [SerializeField] private Animator m_close;

    [SerializeField] private bool m_isCheck;



    void Start()
    {
        m_boxRb = GetComponent<Rigidbody2D>();
        m_boxCol = GetComponent<BoxCollider2D>();
        m_openTimeLine = transform.Find("BoxOpen").gameObject;

    }
    ItemID id;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            m_openTimeLine.SetActive(true);
            id = transform.GetComponent<ItemTest>().Id;
            BoxOpen(itemType);
        }
    }

    void BoxOpen(ItemType type)
    {
        if (!m_isCheck)
        {
            m_close.SetBool("Open", true);
            m_isCheck = true;

            Managers.Spawn.SpawnBox(type, transform.position);

        }

        if (m_isCheck)
        {
            Debug.Log("¹Ú½º ´ÝÇû´Ù");
            m_open.Play("BoxOpen");

        }
    }
}
