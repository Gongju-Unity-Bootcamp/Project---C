using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerST : MonoBehaviour
{
    private float totalAttakState;
    private float totalAttakDelayState;
    private float totalSpeedState;
    private float totalRangeState;

    private float playerAttakState;
    private float playerAttakDelayState;
    private float playerSpeedState;
    private float playerRangeState;

    public float attakDamage { get; set; }
    private float attakDelayTime { get; set; }
    private float moveSpeed { get; set; }
    private float bulletSurviveTime { get; set; }

    private void Start()
    {
        playerAttakState = 3.5f;
        playerAttakDelayState = 2.73f;
        playerRangeState = 3.5f;
        playerSpeedState = 1;

        totalAttakState = 1 + Mathf.Sqrt((float)(playerAttakState * 1.2 + 1)) + 1;
        attakDamage = totalAttakState;

        totalAttakDelayState = 16 - 6 * Mathf.Sqrt((float)(playerAttakDelayState * 1.3 + 1));
        attakDelayTime = totalAttakDelayState * 0.05f;

        totalSpeedState = 1 + playerSpeedState;
        moveSpeed = Mathf.Sqrt((float)(totalSpeedState * 2 + 12));

        totalRangeState = 1 + playerRangeState;
        bulletSurviveTime = Mathf.Sqrt((float)(totalRangeState - 4));
    }

    public void UpdateState(float attakAdd, float attakMulti, float attakSpeedAdd, float attakSpeedMulti, float speed, float range)
    {
        playerAttakState += attakAdd;
        playerAttakDelayState += attakSpeedAdd;



        totalAttakState = ((float)3.5 * Mathf.Sqrt((float)(playerAttakState * 1.2 + 1))) * attakMulti;
        attakDamage = totalAttakState;

        totalAttakDelayState = 16 - 6 * Mathf.Sqrt((float)(attakSpeedMulti * (playerAttakDelayState + attakSpeedAdd) * 1.3 + 1));
        attakDelayTime = totalAttakDelayState * 0.05f;  //어택딜레이 스탯을 유니티 스탯으로 전환

        totalSpeedState += speed;
        moveSpeed = Mathf.Sqrt((float)(totalSpeedState * 2 + 12)); //이동속도 스탯을 유니티 스탯으로 전환

        totalRangeState += range;
        bulletSurviveTime = Mathf.Sqrt((float)(totalRangeState - 4)); //사거리 스탯을 유니티 스탯으로 전환

        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        //totalState로 표시
    }
}
