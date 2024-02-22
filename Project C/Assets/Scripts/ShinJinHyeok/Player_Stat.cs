using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 패시브 아이템을 습득하여 캐릭터의 스탯이 변경되는 스크립트
public class Player_Stat : MonoBehaviour
{
    // 아직 변경하지 않는 스탯
    public float attack;
    public float bulletSpeed;

    public void ApplyItemEffect(Item item)
    {
        // 아이템 효과 적용
        switch (item.ability)
        {
            case Item.Ability.Hp:
                if (Player_Move.hp < 8)
                {
                    Player_Move.hp += 1;
                    Debug.Log("현재 체력 : " + Player_Move.hp);
                }
                break;
            case Item.Ability.MoveSpeed:
                Player_Move.moveSpeed += 0.5f;
                Debug.Log("현재 속도 : " + Player_Move.moveSpeed);
                break;
            case Item.Ability.Attack:
                attack += 10;
                break;
            case Item.Ability.CoolTime:
                if (Player_Attack.cooltime > 0)
                {
                    Player_Attack.cooltime -= 0.1f;
                }
                Debug.Log("쿨타임 : " + Player_Attack.cooltime);
                break;
            case Item.Ability.Range:
                Player_Bullet.bulletSurvieTime += 0.2f;
                Debug.Log("사거리 : " + Player_Bullet.bulletSurvieTime);
                break;
            default:
                break;
        }
    }
}