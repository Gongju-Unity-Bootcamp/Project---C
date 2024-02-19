using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public int ID;
    public string name;
    public ItemType itemType;
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
    //public Ability ability;

    public void init(ItemData ID)
    {

    }



    public enum ItemType
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
