using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private bool isCheck = false;
    private bool isGetItem = false;
    RoomManager roomManager;

    private void Start()
    {
        roomManager = GetComponent<RoomManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCheck)
        {
            //ÄÆ¾À »ðÀÔ

            Managers.Sound.ChangeBGM("BGM_BossRoom");
            isCheck = true;
        }
    }

    private void Update()
    {
        if(!isGetItem && roomManager.RoomAppearance == RoomState.Clear)
        {
            Debug.Log("º¸½º·ëÅ¬¸®¾î");
            Managers.Spawn.SpawnBox(ItemType.GoldenBox, transform.position);
            isGetItem = true;
        }
    }
}
