using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트는 PC가 들고 있어야함
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
        // 액티브 아이템 사용
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_itemInfo == null)
            {
                Debug.Log("액티브 아이템이 없습니다");
            }
            else
            {
                test_Inventory.UseActiveItem(m_itemInfo);
            }
        }
        // 소모형 아이템 사용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_itemInfo == null)
            {
                Debug.Log("소모형 아이템이 없습니다");
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

            Debug.Log("아이템 타입 : " + m_itemInfo.itemType);
            Destroy(collision.gameObject);

            // test_Inventory.Test_GetItem(item); 아이템 습득 테스트를 위해 주석 처리
            // 패시브 아이템 습득 시 스탯 변경
            player_Stat.ApplyItemEffect(m_itemInfo);
        }
    }
}