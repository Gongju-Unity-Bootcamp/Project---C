using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.pool;

public class SpawnManager : MonoBehaviour
{
    //public Pool<OpenBox> Item { get; private set; }


    public void Init()
    {

    }
    public void SpawnBox(ItemType type)
    {
        ItemID id = Manager.Item.GetBox(type);
        //if (Item.id의 프리팹이 풀안에 있으면)
        //{ 활성화 시킴 return;}
        //else { Manager.Resource.에서 새로 생성 후 풀에 넣기 }
    }
}
