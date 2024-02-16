using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� ��ũ��Ʈ�� PC�� ��� �־����.
public class ItemActionController : MonoBehaviour
{
    [SerializeField] private int m_range;

    private Rigidbody2D m_itemRigidbody;

    private void Awake()
    {
        m_itemRigidbody = GetComponent<Rigidbody2D>();
    }

    //Item�� �ߺ� ������� ó���� ����� ���� OnCollisionEnter2D�� �����ؾߵ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            Destroy(collision.gameObject);

            //item�� ȹ��� ó���ϴ� �޼ҵ� �ۼ���

        }
    }
}
