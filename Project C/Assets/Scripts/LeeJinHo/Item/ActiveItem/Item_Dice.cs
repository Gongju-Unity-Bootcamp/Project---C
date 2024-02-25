using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

class Item_Dice : MonoBehaviour, IItem
{
    public IItem m_Iitem { get; set; }
    public ItemType Type { get; set; }

    [SerializeField] private Camera m_Camara = Camera.main;


    public void UsingItems()
    {

        Item[] items = FindObjectsOfType<Item>();

        Rect cameraRect = m_Camara.rect;
        foreach (Item id in items) 
        {
            Vector3 viewportPos = m_Camara.WorldToViewportPoint(id.transform.position);
            if (cameraRect.Contains(viewportPos))
            {
                Type = id.itemType;
                Managers.Spawn.SpawnBox(Type, id.transform.position);

            }
        }
    }
}
