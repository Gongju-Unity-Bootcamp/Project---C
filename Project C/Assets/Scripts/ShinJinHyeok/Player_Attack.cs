using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public GameObject bulletPrefab;
    bool isAttack;
    // Ω∫≈»
    float bulletSpeed = 8.0f;
    float cooltime = 0.2f;
    void Update()
    {
        Vector2 playerPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shoot(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shoot(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shoot(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shoot(Vector2.right);
        }
    }
    void Shoot(Vector2 direction)
    {
        if (!isAttack)
        {
            GameObject bullet = Player_ObjectPooling.instance.GetBulletPool();
            bullet.GetComponent<CircleCollider2D>().enabled = true;

            bullet.transform.position = transform.position;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;

            isAttack = true;
            Invoke("AttackDelay", cooltime);
        }
    }
    void AttackDelay()
    {
        isAttack = false;
    }
}