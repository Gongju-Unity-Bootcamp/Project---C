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
    }
    ItemID id;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !m_isCheck)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (itemType == ItemType.GoldenBox && playerStats.key >0)
            {
                m_openTimeLine.SetActive(true);
                id = transform.GetComponent<Item>().Id;
                BoxOpen(itemType);
                playerStats.UseKey();
            }        
            else if(itemType == ItemType.NormalBox)
            {
                m_openTimeLine.SetActive(true);
                id = transform.GetComponent<Item>().Id;
                BoxOpen(itemType);
            }
        }
    }

    void BoxOpen(ItemType type)
    {
        m_isCheck = true;
        Managers.Spawn.SpawnBox(type, transform.position);
    }
}
