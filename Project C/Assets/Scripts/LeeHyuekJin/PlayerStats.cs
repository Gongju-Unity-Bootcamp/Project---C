using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float totalAttackStats;
    private float totalAttackDelayStats;
    private float totalSpeedStats;
    private float totalRangeStats;

    private float playerAttackStats;
    private float playerAttackDelayStats;
    private float playerSpeedStats;
    private float playerRangeStats;

    public float attackDamage { get; set; }
    public float attackDelayTime { get; set; }
    public float moveSpeed { get; set; }
    public float bulletSurviveTime { get; set; }

    public int cost { get; set; }

    public int key { get; set; }
    public int bomb { get; set; }
    private void Start()
    {
        cost = 4;
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
        bulletSurviveTime = Mathf.Sqrt((float)totalRangeStats);
    }

    public void UpdateStats(float attakAdd, float attakMulti, float attakSpeedAdd, float attakSpeedMulti, float speed, float range)
    {
        playerAttackStats += attakAdd;
        playerAttackDelayStats += attakSpeedAdd;



        totalAttackStats = (3.5f * Mathf.Sqrt((float)(playerAttackStats * 1.2 + 1))) * attakMulti;
        attackDamage = totalAttackStats;

        totalAttackDelayStats = 16 - 6 * Mathf.Sqrt((float)(attakSpeedMulti * (playerAttackDelayStats + attakSpeedAdd) * 1.3 + 1));
        attackDelayTime = totalAttackDelayStats * 0.05f;  //어택딜레이 스탯을 유니티 스탯으로 전환

        totalSpeedStats += speed;
        moveSpeed = Mathf.Sqrt((float)(totalSpeedStats * 2 + 12)); //이동속도 스탯을 유니티 스탯으로 전환

        totalRangeStats += range;
        bulletSurviveTime = Mathf.Sqrt((float)(totalRangeStats - 4)); //사거리 스탯을 유니티 스탯으로 전환

    }
    public void GetKey()
    {
        key++;
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
        cost++;
    }
    public void UpdateAcitiveItem()
    {

    }

}
