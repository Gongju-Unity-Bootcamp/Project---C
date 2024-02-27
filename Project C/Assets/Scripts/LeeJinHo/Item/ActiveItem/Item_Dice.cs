using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class Item_Dice : MonoBehaviour, IItem
{
    public IItem m_Iitem { get; set; }
    public ItemType Type { get; set; }

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        Debug.Log($"보유 코스트수 : {playerStats.cost}");
        if (Input.GetKey(KeyCode.Space) && playerStats.cost >= 3)
        {
            Managers.Sound.EffectSoundChange("Item_Use_Dice");
            UsingItems();
        }
    }

    public void UsingItems()
    {
        Transform go = GameObject.Find(PlayerPrefs.GetString("gameObject")).transform;

        Debug.Log(go.name);
        foreach (Transform child in go)
        {
            if (child.CompareTag("Item"))
            {
                Managers.Spawn.SpawnBox(ItemType.GoldenBox, child.position + Vector3.down);
                Destroy(child.gameObject);
            }
        }
        playerStats.UseActiveItem();
        Debug.Log(playerStats.cost);
    }
}