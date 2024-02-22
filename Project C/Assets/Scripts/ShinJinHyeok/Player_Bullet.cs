using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �����տ� �����Ͽ� �ε����� ������Ʈ�� Ǯ�� ��ȯ�ϴ� ��ũ��Ʈ
public class Player_Bullet : MonoBehaviour
{
    // ����
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