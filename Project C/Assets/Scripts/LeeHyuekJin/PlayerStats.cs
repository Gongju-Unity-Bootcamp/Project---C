using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float totalAttackStats { get; set; }
    public float totalAttackDelayStats { get; set; }
    public float totalSpeedStats { get; set; }
    public float totalRangeStats { get; set; }

    private float playerAttackStats;
    private float playerAttackDelayStats;
    private float playerSpeedStats;
    private float playerRangeStats;

    public float attackDamage { get; set; }
    public float attackDelayTime { get; set; }
    public float moveSpeed { get; set; }
    public float bulletSurviveTime { get; set; }

    public int cost { get; set; }
    public event System.Action StatsChanged;
    public event System.Action KeyBombChanged;
    UIManager _UIManager;
    private int _key;
    public int key
    {
        get { return _key; }
        set
        {
            _key = value;
            KeyBombChanged?.Invoke();
        }
    }
    private int _bomb;
    public int bomb
    {
        get { return _bomb; }
        set
        {
            _bomb = value;
            KeyBombChanged?.Invoke();
        }
    }
    public int hp { get; set; }

    

    private void Awake()
    {
        hp = 8;
        cost = 3;
        key = 1;
        bomb = 1;
        playerAttackStats = 0;
        playerAttackDelayStats = 0;
        playerSpeedStats = 0f;
        playerRangeStats = 1;

        totalAttackStats = 3.5f * Mathf.Sqrt((float)(playerAttackStats * 1.2 + 1)) + 1;
        attackDamage = totalAttackStats;

        totalAttackDelayStats = 16 - 6 * Mathf.Sqrt((float)(playerAttackDelayStats * 1.3 + 1));
        attackDelayTime = totalAttackDelayStats * 0.05f;

        totalSpeedStats = 1 + playerSpeedStats;
        moveSpeed = Mathf.Sqrt((float)(totalSpeedStats * 2 + 12));

        totalRangeStats = 1 + playerRangeStats;
        bulletSurviveTime = Mathf.Sqrt((float)totalRangeStats/2);
    }
    private void Start()
    {
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    public void UpdateStats(float attakAdd, float attakMulti, float attakSpeedAdd, float attakSpeedMulti, float speed, float range)
    {
        playerAttackStats += attakAdd;
        playerAttackDelayStats += attakSpeedAdd;

        totalAttackStats = (3.5f * Mathf.Sqrt((float)(playerAttackStats * 1.2 + 1))) * attakMulti;
        attackDamage = totalAttackStats;

        totalAttackDelayStats = 16 - 6 * Mathf.Sqrt((float)(attakSpeedMulti * (playerAttackDelayStats + attakSpeedAdd) * 1.3 + 1));
        attackDelayTime = totalAttackDelayStats * 0.05f;
        if(attackDelayTime < 0.2f)
        {
            attackDelayTime = 0.1f;
        }
        totalSpeedStats += speed;
        moveSpeed = Mathf.Sqrt((float)(totalSpeedStats * 2 + 12));

        totalRangeStats += range;
        bulletSurviveTime = Mathf.Sqrt((float)(totalRangeStats)/2);

        StatsChanged?.Invoke();
    }
    public void GetKey()
    {
        key++;
        Debug.Log($"보유키: {key}");
    }
    public void UseKey()
    {
        key--;
    }
    public void GetBomb()
    {
        bomb++;
    }
    public void UseBomb()
    {
        bomb--;
    }
    public void ClearRoom()
    {
        if(cost<3)
        {
            cost++;
            _UIManager.UpdateCost(cost);
        }
    }
    public void UseActiveItem()
    {
        cost = 0;
        _UIManager.UpdateCost(cost);
    }
    public void TakeDamage()
    {
        hp--;
        _UIManager.HPController(-1);
        Debug.Log($"현재hp : {hp}");
    }
    public void GetHp(int amount)
    {
        hp += amount;

        _UIManager.HPController(amount);
        Debug.Log($"현재hp : {hp}");
        if (hp > 8)
            hp = 8;
    }
}
