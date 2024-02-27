using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeartType
{
    Heart,
    HalfHeart
}
public class Heart : MonoBehaviour
{
    public HeartType heartType;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerStats.hp < 8)
        {
            if(heartType == HeartType.HalfHeart)
            {
                playerStats.GetHp(1);
            }
            else if(heartType == HeartType.Heart)
            {
                playerStats.GetHp(2);
            }
            Managers.Sound.EffectSoundChange("Item_Get_ConsumableItem");
            Destroy(gameObject);
        }
    }

}
