using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIController;
    [SerializeField] private GameObject Sprites;
    [SerializeField] private GameObject Texts;

    [SerializeField] private Text m_Coin;
    [SerializeField] private Text m_Bomb;
    [SerializeField] private Text m_Key;

    [SerializeField] private Image m_Weapon;
    [SerializeField] private Image m_Active;

    [SerializeField] private Transform m_PlayerHp;
    [SerializeField] private GameObject[] m_HpImage;

    [SerializeField] private float Attack;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float Speed;
    [SerializeField] private float Range;
    private const int MAX_HP_BAR = 8;
    private int HpBar = MAX_HP_BAR - 1;

    public void Init(PlayerStats playerStats)
    {
        UIController = GameObject.FindWithTag("UIController");
        Sprites = UIController.transform.Find("Sprites").gameObject;
        Texts   = UIController.transform.Find("Texts").gameObject;

        m_Coin = Texts.transform.Find("CoinText").GetComponent<Text>();
        m_Bomb = Texts.transform.Find("BombText").GetComponent<Text>();
        m_Key  = Texts.transform.Find("KeyText").GetComponent<Text>();

        m_Weapon = Sprites.transform.Find("WeaponSprite").GetComponent<Image>();
        m_Active = Sprites.transform.Find("ActiveSprite").GetComponent<Image>();

        m_PlayerHp = UIController.transform.Find("PlayerHp");
        m_HpImage = new GameObject[MAX_HP_BAR];
        for (int i = 0; i < MAX_HP_BAR; ++i)
        {
            GameObject go = m_PlayerHp.transform.Find($"Life ({i})").gameObject;
            m_HpImage[i] = go;
        }
        
        playerStats.KeyBombChanged += GetConsumer;
        playerStats.StatsChanged += GetPassive;
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


    public void HPController(int hp)
    {
        Debug.Log($"hp : {hp}");


        Debug.Log(HpBar + hp);
        Debug.Log(MAX_HP_BAR + 1);
        if (HpBar + hp > 7) { hp = 1; }
        
        switch (hp)
        {
            case -1:
                m_HpImage[HpBar].SetActive(false);
                break;
            case 1:
                HpBar++;
                m_HpImage[HpBar].SetActive(true);
                HpBar--;
                break;
            case 2:
                HpBar++;
                Debug.Log($"케이스2-1{HpBar}");
                m_HpImage[HpBar].SetActive(true);
                HpBar++;
                Debug.Log($"케이스2-2{HpBar}");
                m_HpImage[HpBar].SetActive(true);
                HpBar -= 2;
                break;
        }
        HpBar += hp;
    }


}
