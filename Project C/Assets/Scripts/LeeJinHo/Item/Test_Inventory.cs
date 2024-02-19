using System.Collections;
using System.Collections.Generic;
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
    private int m_GrenadeCount = 1;
    private int m_KeyCount = 0;

    [SerializeField] private Image m_Active;
    [SerializeField] private Image m_Weapon;

    [SerializeField] private Transform m_HpController;  // 체력(HP) 관리할 객체
    [SerializeField] private GameObject[] m_Hp;         // 체력 오브젝트들

    [SerializeField] private Player_Move m_PlayerMove;

    public GameObject bombPrefab;

    IEnumerator ActiveAngel(float invincibleTime)
    {
        // 액티브 아이템 '천사' 사용 시 무적 상태를 수행한다
        Player_Move.isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        Player_Move.isInvincible = false;
    }
    private void Awake()
    {
        for (int i = 0; i < m_HpController.childCount; ++i)
        {
            if (i == m_HpController.childCount - 2) break;
            GameObject hp = m_HpController.transform.Find($"Life ({i})").gameObject;
            m_Hp[i] = hp;
        }
    }
    public void Test_GetItem(Item item)
    {
        // 패시브 아이템은 ItemActionController 스크립트에서 Player_Stat 스크립트로 전달한다

        // 액티브 아이템
        if (item.itemType == ItemType.Active)
        {
            m_Active.sprite = item.sprite;
        }
        // 소모형 아이템
        if (item.itemType == ItemType.Consumer)
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
        // 액티브 아이템을 사용할 경우 여기서 아이템을 구분하여 기능을 수행
        if (item.name == "Angel")
        {
            // 액티브 아이템 '천사'
            Debug.Log("천사 강림!");
            StartCoroutine(ActiveAngel(5.0f));
            ItemActionController.m_itemInfo = null;
        }
    }
    public void UseConsumerItem(Item item)
    {
        if (item.name == "Grenade")
        {
            if (m_GrenadeCount > 0)
            {
                // 소모형 아이템 '폭탄'
                Debug.Log("뿌직!");
                Vector3 bombPosition = m_PlayerMove.GetPlayerPosition();
                Instantiate(bombPrefab, bombPosition, Quaternion.identity);
                m_GrenadeCount--;
                Debug.Log("현재 남은 폭탄 개수 : " + m_GrenadeCount);
            }
        }
    }
    // Hp 관리
    #region
    private int m_HpCount = 7;
    public void Test_TakeDamager()
    {
        m_Hp[m_HpCount].SetActive(false);
        --m_HpCount;
    }

    public void Test_HpPlus()
    {
        m_Hp[m_HpCount].SetActive(true);
        ++m_HpCount;
    }
    #endregion
}