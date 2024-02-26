using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private bool isCheck = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCheck)
        {
            //ÄÆ¾À »ðÀÔ

            Managers.Sound.ChangeBGM("BGM_BossRoom");
            isCheck = true;
        }
    }
}
