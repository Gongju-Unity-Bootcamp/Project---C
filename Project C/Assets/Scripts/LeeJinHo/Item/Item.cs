using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string name;
    public int cost;

    //Item�� ���������� ���� Ȱ���� ��� �ʿ� ����.
    //�������� Item�� �̹���, ���带 ������ ��� ���.
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

}
