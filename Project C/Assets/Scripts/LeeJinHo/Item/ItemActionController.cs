using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//이 스크립트는 PC가 들고 있어야함.
public class ItemActionController : MonoBehaviour
{
    [SerializeField] private int m_range;

    private Rigidbody2D m_itemRigidbody;

    private void Awake()
    {
        m_itemRigidbody = GetComponent<Rigidbody2D>();
    }

    //Item을 중복 습득시의 처리할 방법에 따라 OnCollisionEnter2D로 변경해야됨.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            Destroy(collision.gameObject);

            //item을 획득시 처리하는 메소드 작성란

        }
    }
}
