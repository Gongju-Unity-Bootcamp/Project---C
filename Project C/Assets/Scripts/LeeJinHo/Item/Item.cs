using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//없애야 되는 클래스

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string name;
    public ItemTypes itemType;
    public float Attack;
    public float AttackSpeed;
    public float Speed;
    public float Range;
    public int Cost;
    public string Sprite;
    public string AcquireSound;
    public string UseSound;
    
    public Sprite sprite;
    public AudioSource audioSource;

    public Ability ability;
    public enum ItemTypes
    {
        Passive,
        Active,
        Consumer
    }
    public enum Ability
    {
        None,
        Hp,
        MoveSpeed,
        Attack,
        CoolTime,
        Range
    }
}
