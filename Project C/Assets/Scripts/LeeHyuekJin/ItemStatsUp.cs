using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatsUp : MonoBehaviour
{
    PlayerStats playerStats;
    AudioSource audioSource;
    Collider2D collider2D;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerStats.UpdateStats(1, 1, 1, 1, 1, 1);
            collider2D.enabled = false;
            transform.Rotate(90, 0, 0);
            audioSource.Play();
        }
    }
}
