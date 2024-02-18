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

    [SerializeField] private Transform m_HpController; //Hp(빨간색) 관리할 객체
    [SerializeField] private GameObject[] m_Hp; //체력 오브젝트들

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
        if (item.itemType == ItemType.Passive)
        {
            //test_Player_Move.
            //HP전달 메소드
        }

        if (item.itemType == ItemType.Active)
        {
            m_Active.sprite = item.sprite;
        }

        if (item.itemType == ItemType.Consumer)
        {
            if (item.name == "Coin")
            {
                m_CoinCount++;
                m_Coin.text = m_CoinCount.ToString();
            }
            if (item.name == "Key")
            {
                m_KeyCount++;
                m_Key.text = m_KeyCount.ToString();
            }
            if (item.name == "Grenade")
            {
                m_GrenadeCount++;
                m_Grenade.text = m_GrenadeCount.ToString();
            }
        }
    }


    //Hp 관리
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
