using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Util;
using Util.pool;

public class SpawnManager : MonoBehaviour
{
    ObjectPool<GameObject> ItemPool;
    ObjectPool<AudioClip> ClipPool;
    ObjectPool<AnimationClip> AinmClip;

    public void Init()
    {

    }
    public void SpawnBox(ItemType type)
    {
        CreatItem(Manager.Item.BoxClassification(type));
    }

    private GameObject CreatItem(ItemID id)
    {
        Debug.Log($"CreatItem :{id}");
        ItemData data = Manager.Data.Item[id];
        Debug.Log($"CreatItem :{data}");
        GameObject go = Manager.Resource.Instantiate(data.Sprite);
        ItemTest itemTest = go.AddComponent<ItemTest>();
        itemTest.Init(id);

        return go;
    }
}
