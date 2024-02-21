using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ����Ű �Է��� �޾� ĳ���� �����Ӱ��� ������ ������ �����ϴ� ��ũ��Ʈ
public class Player_Attack : MonoBehaviour
{
    bool isAttack;
    // ����
    [SerializeField]float bulletSpeed = 8.0f;
    public static float cooltime = 0.4f;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Shoot(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Shoot(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Shoot(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Shoot(Vector2.left);
        }
    }
    void Shoot(Vector2 direction)
    {
        if (!isAttack && Player_Move.gameState == "playing")
        {
            GameObject bullet = Player_ObjectPooling.instance.GetBulletPool();
            Rigidbody2D bulletRbody = bullet.GetComponent<Rigidbody2D>();

            bullet.transform.position = transform.position;
            bulletRbody.velocity = direction * bulletSpeed;

            isAttack = true;
            Invoke("AttackDelay", cooltime);
        }
    }
    void AttackDelay()
    {
        isAttack = false;
    }
}