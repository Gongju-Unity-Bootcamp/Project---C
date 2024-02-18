using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//이 스크립트는 PC가 들고 있어야함.
public class ItemActionController : MonoBehaviour
{
    [SerializeField] private int m_range;
    [SerializeField] private int GrenadeCount;
    [SerializeField] private int CoinCount;
    [SerializeField] private int KeyCount;
    
    private CircleCollider2D m_collider;
    private Rigidbody2D m_itemRigidbody;

    [SerializeField] Test_Inventory test_Inventory;


    private void Awake()
    {
        m_itemRigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
    }

    //Item을 중복 습득시의 처리할 방법에 따라 OnCollisionEnter2D로 변경해야됨.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("콜라이더");
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("아이템");
            Item item = collision.transform.GetComponent<ItemPickUp>().item;
            Destroy(collision.gameObject);

            test_Inventory.Test_GetItem(item);

        }
    }
}
