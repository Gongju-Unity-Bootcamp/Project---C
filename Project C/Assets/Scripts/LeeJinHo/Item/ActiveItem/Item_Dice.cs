using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

class Item_Dice : MonoBehaviour, IItem
{
    public IItem m_Iitem { get; set; }
    public ItemType Type { get; set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UsingItems();
        }
    }

    public void UsingItems()
    {
        Transform go = GameObject.Find(PlayerPrefs.GetString("gameObject")).transform;

        Debug.Log(go.name);
        foreach(Transform child in go)
        {
            if(child.CompareTag("Item"))
            {
                Managers.Spawn.SpawnBox(ItemType.GoldenBox, go.position);
                Destroy(child.gameObject);
            }
        }
    }
}
