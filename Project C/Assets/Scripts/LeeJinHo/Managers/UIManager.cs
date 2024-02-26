using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIController;
    [SerializeField] private GameObject Sprites;
    [SerializeField] private GameObject Texts;
    [SerializeField] private GameObject Stats;
    

    [SerializeField] private Text m_Coin;
    [SerializeField] private Text m_Bomb;
    [SerializeField] private Text m_Key;

    [SerializeField] private Image m_Weapon;
    [SerializeField] private Image m_Active;

    [SerializeField] private Transform m_PlayerHp;
    [SerializeField] private GameObject[] m_HpImage;

    [SerializeField] private Text Attack;
    [SerializeField] private Text AttackCoolTime;
    [SerializeField] private Text Speed;
    [SerializeField] private Text Range;

    [SerializeField] private Slider bossHPSlider;
    public GameObject BossHp { get; set; }
    private const int MAX_HP_BAR = 8;
    private int HpBar = MAX_HP_BAR - 1;

    public void Init(PlayerStats playerStats)
    {
        UIController = GameObject.FindWithTag("UIController");
        Sprites = UIController.transform.Find("Sprites").gameObject;
        Texts   = UIController.transform.Find("Texts").gameObject;
        BossHp = UIController.transform.Find("BoosHp").gameObject;

        m_Coin = Texts.transform.Find("CoinText").GetComponent<Text>();
        m_Bomb = Texts.transform.Find("BombText").GetComponent<Text>();
        m_Key  = Texts.transform.Find("KeyText").GetComponent<Text>();

        m_Weapon = Sprites.transform.Find("WeaponSprite").GetComponent<Image>();
        m_Active = Sprites.transform.Find("ActiveSprite").GetComponent<Image>();

        m_PlayerHp = UIController.transform.Find("PlayerHp");
        m_HpImage = new GameObject[MAX_HP_BAR];

        Stats = GameObject.Find("Stats");

        Attack = Stats.transform.Find("attack").GetComponent<Text>();
        AttackCoolTime = Stats.transform.Find("cooltime").GetComponent<Text>();
        Speed = Stats.transform.Find("moveSpeed").GetComponent<Text>();
        Range = Stats.transform.Find("range").GetComponent<Text>();

        bossHPSlider = BossHp.GetComponent<Slider>();
        for (int i = 0; i < MAX_HP_BAR; ++i)
        {
            GameObject go = m_PlayerHp.transform.Find($"Life ({i})").gameObject;
            m_HpImage[i] = go;
        }
        
        playerStats.KeyBombChanged += GetConsumer;
        playerStats.StatsChanged += GetPassive;

        GameObject pasue = GameObject.Find("Pause");
        BossHp.SetActive(false);
        pasue.SetActive(false);
    }

    public void GetConsumer()
    {
        m_Key.text = $"X {Managers.PlayerStats.key}";
        m_Bomb.text = $"X {Managers.PlayerStats.bomb}";
    }
    public void GetActive(string item)
    {
        m_Active.sprite = Managers.Resource.LoadSprite(item);
    }
    public void GetPassive()
    {
        Attack.text = $"{Managers.PlayerStats.totalAttackStats}";
        AttackCoolTime.text = $"{Managers.PlayerStats.totalAttackDelayStats}";
        Speed.text = $"{Managers.PlayerStats.totalSpeedStats}";
        Range.text = $"{Managers.PlayerStats.totalRangeStats}";
    }


    public void HPController(int hp)
    {
        Debug.Log($"hp : {hp}");


        Debug.Log(HpBar + hp);
        if (HpBar + hp > MAX_HP_BAR-1) { hp = 1; }
        
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
                m_HpImage[HpBar].SetActive(true);
                HpBar++;
                m_HpImage[HpBar].SetActive(true);
                HpBar -= 2;
                break;
        }
        HpBar += hp;
    }

    public void UpdateBossHP(float currentHP, float maxHP)
    {
        bossHPSlider.value = Mathf.Clamp01(currentHP / maxHP);
    }
}
