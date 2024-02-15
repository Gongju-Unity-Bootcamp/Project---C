using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string name;
    public int cost;

    //Item을 프리팹으로 만들어서 활용할 경우 필요 없음.
    //동적으로 Item의 이미지, 사운드를 변경할 경우 사용.
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
