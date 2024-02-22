using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �нú� �������� �����Ͽ� ĳ������ ������ ����Ǵ� ��ũ��Ʈ
public class Player_Stat : MonoBehaviour
{
    // ���� �������� �ʴ� ����
    public float attack;
    public float bulletSpeed;

    public void ApplyItemEffect(Item item)
    {
        // ������ ȿ�� ����
        switch (item.ability)
        {
            case Item.Ability.Hp:
                if (Player_Move.hp < 8)
                {
                    Player_Move.hp += 1;
                    Debug.Log("���� ü�� : " + Player_Move.hp);
                }
                break;
            case Item.Ability.MoveSpeed:
                Player_Move.moveSpeed += 0.5f;
                Debug.Log("���� �ӵ� : " + Player_Move.moveSpeed);
                break;
            case Item.Ability.Attack:
                attack += 10;
                break;
            case Item.Ability.CoolTime:
                if (Player_Attack.cooltime > 0)
                {
                    Player_Attack.cooltime -= 0.1f;
                }
                Debug.Log("��Ÿ�� : " + Player_Attack.cooltime);
                break;
            case Item.Ability.Range:
                Player_Bullet.bulletSurvieTime += 0.2f;
                Debug.Log("��Ÿ� : " + Player_Bullet.bulletSurvieTime);
                break;
            default:
                break;
        }
    }
}