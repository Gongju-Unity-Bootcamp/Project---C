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

    public static Item m_itemInfo;

    [SerializeField] Test_Inventory test_Inventory;
    [SerializeField] Player_Stat player_Stat;

    private void Awake()
    {
        m_itemRigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        // ��Ƽ�� ������ ���
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_itemInfo == null)
            {
                Debug.Log("��Ƽ�� �������� �����ϴ�");
            }
            else
            {
                test_Inventory.UseActiveItem(m_itemInfo);
            }
        }
        // �Ҹ��� ������ ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_itemInfo == null)
            {
                Debug.Log("�Ҹ��� �������� �����ϴ�");
            }
            else
            {
                test_Inventory.UseConsumerItem(m_itemInfo);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            m_itemInfo = collision.transform.GetComponent<ItemPickUp>().item;

            Debug.Log("������ Ÿ�� : " + m_itemInfo.itemType);
            Destroy(collision.gameObject);

            // test_Inventory.Test_GetItem(item); ������ ���� �׽�Ʈ�� ���� �ּ� ó��
            // �нú� ������ ���� �� ���� ����
            player_Stat.ApplyItemEffect(m_itemInfo);
        }
    }
}