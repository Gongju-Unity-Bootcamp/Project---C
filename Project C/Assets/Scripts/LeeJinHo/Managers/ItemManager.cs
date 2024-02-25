using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager
{
    /// <summary>
    /// 아이템 갯수 체크하는 테이블 추가시 변경 예정
    /// </summary>
    private const int PASSIVE_ITEM_TABLE_ID_START = 1001;
    //private const int PASSIVE_ITEM_TABLE_ID_END = 1022;
    //private const int ACTIVE_ITEM_TABLE_ID_START = 2001;
    private const int ACTIVE_ITEM_TABLE_ID_END = 1022;
    private const int CONSUMER_ITEM_TABLE_ID_START = 3001;
    private const int CONSUMER_ITEM_TABLE_ID_END = 3003;


    public void Init()
    {

    }

    //룸매니저에게 전달.
    //플레이어가 상자를 획득 했을때 받는 메소드
    //매개변수를 획득한 상자의 ID를 받아와서 상자의 종류를 구분하고
    //상자의 타입에 따라 뱉어낼 아이템의 타입을 구분짓는다.
    public ItemID OpenBox(ItemType type)
    {
        var boxType = type switch
        {
            ItemType.NormalBox => GetItemType(ItemType.Consumer),
            ItemType.GoldenBox => GetItemType(ItemType.Active, ItemType.Passive),
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
        return GetItemID(boxType);
    }

    //위에서 정해진 2가지 타입중 랜덤으로 하나를 골라서 선택한 뒤 선택한 
    private ItemType GetItemType(ItemType type1, ItemType type2 = ItemType.None)
    {
        if (type2 == ItemType.None)
        {
            return type1;
        }

        ItemType type = Random.value < Random.Range(0, 2) ? type1 : type2;
        return type;

        #region
        //int random = Random.Range(0, 2);
        // ItemType type = Random.value < 0.5f ? type1[0].Item1 : type1[1].Item1;

        //int totalWeight = 0;
        //foreach(var (_, weight) in type1) 
        //{ 
        //    totalWeight += weight;
        //}

        //int roll = Random.Range(0, totalWeight);
        //foreach (var (itemType, weight) in type1)
        //{
        //    if (roll < weight) { return itemType; }
        //    roll -= weight;
        //}

        //throw new InvalidCastException("Railed to GetItemType");
        #endregion
    }


    //정해진 타입에 따라 
    private ItemID GetItemID(ItemType type) 
    {
        int roll = type switch
        {
            ItemType.None => 0,
            ItemType.Passive => Random.Range(PASSIVE_ITEM_TABLE_ID_START, ACTIVE_ITEM_TABLE_ID_END),
            ItemType.Active => Random.Range(PASSIVE_ITEM_TABLE_ID_START, ACTIVE_ITEM_TABLE_ID_END),
            ItemType.Consumer => Random.Range(CONSUMER_ITEM_TABLE_ID_START, CONSUMER_ITEM_TABLE_ID_END),
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
        ItemID itemId = (ItemID)roll;
        return itemId;
    }
}
