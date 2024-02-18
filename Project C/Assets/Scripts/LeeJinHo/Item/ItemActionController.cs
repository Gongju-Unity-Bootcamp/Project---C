using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ũ��Ʈ�� PC�� ��� �־����
public class ItemActionController : MonoBehaviour
{
    [SerializeField] private int m_range;
    [SerializeField] private int GrenadeCount;
    [SerializeField] private int CoinCount;
    [SerializeField] private int KeyCount;
    
    private CircleCollider2D m_collider;
    private Rigidbody2D m_itemRigidbody;

    [SerializeField] Test_Inventory test_Inventory;
    [SerializeField] Player_Stat player_Stat;

    private void Awake()
    {
        m_itemRigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
    }

    // ������ �ߺ� ������� ó���� ����� ���� OnCollisionEnter2D�� �����ؾߵ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.transform.GetComponent<ItemPickUp>().item;
            Destroy(collision.gameObject);
            
            // test_Inventory.Test_GetItem(item); ������ ���� �׽�Ʈ�� ���� �ּ� ó��
            player_Stat.ApplyItemEffect(item);
        }
    }
}