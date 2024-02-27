using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private bool isCheck = false;
    private bool isGetItem = false;
    RoomManager roomManager;

    public GameObject boss;
    private GameObject cutScean;
    private void Start()
    {
        cutScean = GameObject.Find("BossCutScene");
        roomManager = GetComponent<RoomManager>();
        cutScean.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCheck)
        {
            //ÄÆ¾À »ðÀÔ
            Managers.UI.BossHp.SetActive(true);
            isCheck = true;
            StartCoroutine(CreateBoss());
        }
    }

    private void Update()
    {
        /*if (!isGetItem && roomManager.RoomAppearance == RoomState.Clear)
        {
            Managers.Spawn.SpawnBox(ItemType.GoldenBox, transform.position);
            isGetItem = true;
            Managers.UI.BossHp.SetActive(false);
        }*/
    }
    IEnumerator CreateBoss()
    {
        cutScean.SetActive(true);
        yield return new WaitForSeconds(4.2f);
        cutScean.SetActive(false);
        yield return new WaitForSeconds(0.45f);
        GameObject newBoss = Instantiate(boss, transform.position, Quaternion.identity, transform);
    }
}
