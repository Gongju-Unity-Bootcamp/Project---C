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
    private bool isGetHerat = false;
    // Start is called before the first frame update
    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"ÇöÀçhp:{playerStats.hp}");
        if(!isGetHerat && collision.gameObject.CompareTag("Player") && playerStats.hp < 8)
        {
            isGetHerat = true;
            Managers.Sound.ChangeGetItemSound("Item_Get_ConsumableItem");
            if (heartType == HeartType.HalfHeart)
            {
                playerStats.GetHp(1);
                Destroy(gameObject);
            }
            else if(heartType == HeartType.Heart)
            {
                playerStats.GetHp(2);
                Destroy(gameObject);
            }
            
            
        }
    }

}
