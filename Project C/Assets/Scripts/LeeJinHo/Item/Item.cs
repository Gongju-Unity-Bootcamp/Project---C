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

    // �������� ���������� ���� Ȱ���� ��쿡�� �ʿ䰡 ����
    // �������� �������� �̹���, ���带 ������ ��쿡 ���
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
