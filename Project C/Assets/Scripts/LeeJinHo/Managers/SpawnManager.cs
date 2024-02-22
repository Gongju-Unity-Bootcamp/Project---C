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
    public void SpawnBox(ItemType type, Vector3 position)
    {
        CreatItem(Managers.Item.OpenBox(type), position);
    }

    private GameObject CreatItem(ItemID id, Vector3 po)
    {
        Debug.Log($"CreatItem :{id}");
        ItemData data = Managers.Data.Item[id];
        Debug.Log($"CreatItem :{data}");
        GameObject go = Managers.Resource.Instantiate(data.Sprite);
        ItemTest itemTest = go.AddComponent<ItemTest>();
        go.transform.position = po;
        itemTest.Init(id);

        return go;
    }
}
