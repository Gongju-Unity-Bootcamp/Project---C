using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Text m_Coin;
    [SerializeField] private Text m_Bomb;
    [SerializeField] private Text m_Key;

    [SerializeField] private float Attack;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float Speed;
    [SerializeField] private float Range;

    [SerializeField] private Image m_Weapon;
    [SerializeField] private Image m_Active;

    [SerializeField] private Transform m_PlayerHp;
    [SerializeField] private GameObject[] m_HpImage;

    public void Init()
    {
        //m_HpConTroller는 유니티 인스펙터창에서 PlayerHp오브젝트 드래그앤 드랍
        /*for (int i = 0; i < m_PlayerHp.childCount - 2; ++i)
        {
            GameObject hp = m_PlayerHp.transform.Find($"Life ({i})").gameObject;
            m_HpImage[i] = hp;
        }*/
    }

    public void GetConsumer()
    {
        m_Key.text = $"X {Managers.PlayerStats.key}";
        m_Bomb.text = $"X {Managers.PlayerStats.bomb}";
    }
    public void GetActive(Item item)
    {
        m_Active.sprite = Managers.Resource.LoadSprite(item.Sprite);
    }
    public void GetPassive()
    {
        Attack = Managers.PlayerStats.totalAttackStats;
        AttackSpeed = Managers.PlayerStats.totalAttackDelayStats;
        Speed = Managers.PlayerStats.totalSpeedStats;
        Range = Managers.PlayerStats.totalRangeStats;
    }

    private const int MAX_HP_BAR = 8;
    private int HpBar = MAX_HP_BAR - 1;
    public void HPController(int hp)
    {
        if (HpBar + hp == MAX_HP_BAR)
        { return; }

        HpBar += hp;

        switch (hp)
        {
            case -1:
                m_HpImage[HpBar].SetActive(false);
                break;
            case 1:
                m_HpImage[HpBar].SetActive(true);
                break;
        }
    }


}
