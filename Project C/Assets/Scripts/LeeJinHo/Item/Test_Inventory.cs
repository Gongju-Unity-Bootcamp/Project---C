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

    [SerializeField] private Test_Player_Move Test_Player_Move;

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
            // 임시로 게임 종료 기능을 넣어놓음
            Player_GameManager.instance.GameOver();
        }
    }
    public void UseConsumerItem(Item item)
    {
        if (item.name == "Grenade")
        {
            // 폭탄 프리팹? 이미지를 생성하고 폭발시키는 기능을 수행
            // 임시로 게임 종료 기능을 넣어놓음
            Player_GameManager.instance.GameOver();
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