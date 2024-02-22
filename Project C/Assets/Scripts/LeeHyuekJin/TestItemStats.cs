using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemStats : MonoBehaviour
{
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerStats.UpdateStats(2, 2, 4, 5, 6, 1);

            Destroy(gameObject);
        }
    }
}
