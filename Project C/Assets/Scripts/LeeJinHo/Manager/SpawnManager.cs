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
        //if (Item.id�� �������� Ǯ�ȿ� ������)
        //{ Ȱ��ȭ ��Ŵ return;}
        //else { Manager.Resource.���� ���� ���� �� Ǯ�� �ֱ� }
    }
}
