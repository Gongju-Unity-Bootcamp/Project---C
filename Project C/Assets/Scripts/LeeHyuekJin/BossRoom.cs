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
    private GameObject bossSpawnEffect;
    private void Start()
    {
        cutScean = GameObject.Find("BossCutScene");
        roomManager = GetComponent<RoomManager>();
        cutScean.SetActive(false);
        bossSpawnEffect = GameObject.Find("BossSpawnEffect");
        bossSpawnEffect.SetActive(false);
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

    IEnumerator CreateBoss()
    {
        Managers.Sound.EffectSoundChange("Sound_Map_BossIntro");
        cutScean.SetActive(true);
        yield return new WaitForSeconds(4.2f);
        cutScean.SetActive(false);
        bossSpawnEffect.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        GameObject newBoss = Instantiate(boss, transform.position, Quaternion.identity, transform);
    }
}
