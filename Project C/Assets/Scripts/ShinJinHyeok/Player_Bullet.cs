using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 눈물 프리팹에 부착하여 부딪히면 오브젝트를 풀에 반환하는 스크립트
public class Player_Bullet : MonoBehaviour
{
    // 스탯
    public float attack = 10.0f;
    public static float range = 1.2f;
    void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterRange());
    }
    IEnumerator ReturnBulletAfterRange()
    {
        yield return new WaitForSeconds(range);
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