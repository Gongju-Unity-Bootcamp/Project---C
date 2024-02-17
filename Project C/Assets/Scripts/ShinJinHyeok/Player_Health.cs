using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Health : MonoBehaviour
{
    public static int hp;
    public static bool isInvincible;

    public abstract void TakeDamage();
}