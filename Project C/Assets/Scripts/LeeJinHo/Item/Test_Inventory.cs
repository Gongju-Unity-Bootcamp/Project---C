using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static Item;

public class Test_Inventory : MonoBehaviour
{
    [SerializeField] private Text m_Coin;
    [SerializeField] private Text m_Grenade;
    [SerializeField] private Text m_Key;

    private int m_CoinCount = 0;
    [SerializeField] private int m_GrenadeCount = 1;
    private int m_KeyCount = 0;

    [SerializeField] private Image m_Active;
    [SerializeField] private Image m_Weapon;

    [SerializeField] private Transform m_HpController;  // ü��(HP) ������ ��ü
    [SerializeField] public GameObject[] m_Hp;          // ü�� ������Ʈ��

    [SerializeField] private Player_Move m_PlayerMove;

    public GameObject bombPrefab;

    IEnumerator ActiveAngel(float invincibleTime)
    {
        // ��Ƽ�� ������ 'õ��' ��� �� ���� ���¸� �����Ѵ�
        Player_Move.isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        Player_Move.isInvincible = false;
    }
    private void Awake()
    {
        for (int i = 0; i < m_Hp.Length; ++i)
        {
            GameObject hp = m_HpController.transform.Find($"Life ({i})").gameObject;
            m_Hp[i] = hp;
        }
    }
    public void Test_GetItem(Item item)
    {
        // �нú� �������� ItemActionController ��ũ��Ʈ���� Player_Stat ��ũ��Ʈ�� �����Ѵ�

        // ��Ƽ�� ������
        if (item.itemType == ItemTypes.Active)
        {
            m_Active.sprite = item.sprite;
        }
        // �Ҹ��� ������
        if (item.itemType == ItemTypes.Consumer)
        {
            if (item.name == "Coin")
            {
                m_CoinCount++;
                m_Coin.text = m_CoinCount.ToString();
            }
            if (item.name == "Grenade")
            {
                m_GrenadeCount++;
                m_Grenade.text = m_GrenadeCount.ToString();
            }
            if (item.name == "Key")
            {
                m_KeyCount++;
                m_Key.text = m_KeyCount.ToString();
            }
        }
    }
    public void UseActiveItem(Item item)
    {
        // ��Ƽ�� �������� ����� ��� ���⼭ �������� �����Ͽ� ����� ����
        if (item.name == "Angel")
        {
            // ��Ƽ�� ������ 'õ��'
            Debug.Log("õ�� ���");
            StartCoroutine(ActiveAngel(5.0f));
            // ������ ��뿩�� bool�� �ϳ� �����
            ItemActionController.m_itemInfo = null;
        }
    }
    public void UseConsumerItem(Item item)
    {
        if (item.name == "Grenade")
        {
            if (m_GrenadeCount > 0)
            {
                // �Ҹ��� ������ '��ź'
                Debug.Log("��ź ���");
                Transform playerPosition = GameObject.FindWithTag("Player").transform;
                Vector2 bombPosition = playerPosition.position;

                Instantiate(bombPrefab, bombPosition, Quaternion.identity);
                m_GrenadeCount--;
                Debug.Log("���� ���� ��ź ���� : " + m_GrenadeCount);
            }
        }
    }
    // Hp ����
    #region
    private int m_HpCount = 7;
    public void Test_TakeDamager()
    {
        Debug.Log("UI ��Ʈ ������Ʈ");
        m_Hp[m_HpCount].SetActive(false);
        m_HpCount--;
    }
    public void Test_HpPlus()
    {
        m_Hp[m_HpCount].SetActive(true);
        ++m_HpCount;
    }
    #endregion
}