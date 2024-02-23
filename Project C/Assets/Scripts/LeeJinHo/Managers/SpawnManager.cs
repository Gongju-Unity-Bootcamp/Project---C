using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Util;
using Util.pool;

public class SpawnManager : MonoBehaviour
{
    //ObjectPool<GameObject> ItemPool;
    //ObjectPool<AudioClip> ClipPool;
    //ObjectPool<AnimationClip> AinmClip;

    public Stack<GameObject> m_Items;

    public void Init()
    {
        m_Items = new Stack<GameObject>();
    }
    public void SpawnBox(ItemType type, Vector3 position)
    {
        Creat(Managers.Item.OpenBox(type), position);
    }

    private GameObject CreatItem(ItemID id, Vector3 po)
    {
        ItemData data = Managers.Data.Item[id];
        GameObject go = Managers.Resource.Instantiate(data.Sprite);
        ItemTest itemTest = go.AddComponent<ItemTest>();
        go.transform.position = po;
        itemTest.Init(id, po);

        return go;
    }

    public void Creat(ItemID id, Vector3? po = null)
    {
        GameObject go;
        
        if (m_Items.Count == 0) 
        {
            go = new GameObject(nameof(ItemTest));
            go.AddComponent<ItemTest>().Init(id, (Vector3)po);
            return;
        }

        go = m_Items.Pop();
        go.SetActive(true);
        go.GetComponent<ItemTest>().Init(id, (Vector3)po);
    }
}
