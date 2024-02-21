using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public new string name;
    public int cost;
    public ItemType itemType;
    public Ability ability;

    // 아이템을 프리팹으로 만들어서 활용할 경우에는 필요가 없음
    // 동적으로 아이템의 이미지, 사운드를 변경할 경우에 사용
    #region
    public Sprite sprite;
    public AudioClip clip;
    public AudioSource audioSource;
    #endregion
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
