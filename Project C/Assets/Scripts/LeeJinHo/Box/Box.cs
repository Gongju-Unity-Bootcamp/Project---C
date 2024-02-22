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

    //[SerializeField] private List<GameObject> items;
    [SerializeField] private Transform itemDropPoint;


    void Start()
    {
        m_boxRb = GetComponent<Rigidbody2D>();
        m_boxCol = GetComponent<BoxCollider2D>();
        m_openTimeLine = transform.Find("BoxOpen").gameObject;
        //m_open = GetComponent<Animator>();
        //m_close = GetComponent<Animator>();
        //itemType = ItemType.NormalBox;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_openTimeLine.SetActive(true);
            itemType = ItemType.NormalBox;
            BoxOpen(itemType);
        }
    }

    void BoxOpen(ItemType type)
    {
        ItemType Itype = ItemType.NormalBox;
        if (!m_isCheck)
        {
            m_close.SetBool("Open", true);
            m_isCheck = true;
            Managers.Spawn.SpawnBox(type, transform.position);
        }

        if (m_isCheck)
        {
            m_open.Play("BoxOpen");

        }
    }
}
