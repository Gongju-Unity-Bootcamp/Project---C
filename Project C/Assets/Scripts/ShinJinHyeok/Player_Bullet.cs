using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 눈물 프리팹에 부착하여 부딪히면 오브젝트를 풀에 반환하는 스크립트
public class Player_Bullet : MonoBehaviour
{
    // 스탯
    public float attackDamage;
    public static float bulletSurvieTime { get; set; }

    private PlayerStats playerStats;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void Update()
    {
        attackDamage = playerStats.attackDamage;
        bulletSurvieTime = playerStats.bulletSurviveTime;
    }
    void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterRange());
    }
    IEnumerator ReturnBulletAfterRange()
    {
        yield return new WaitForSeconds(bulletSurvieTime);
        ReturnBullet();
    }
    void ReturnBullet()
    {
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player_ObjectPooling.instance.ReturnBulletPool(gameObject);
    }
}