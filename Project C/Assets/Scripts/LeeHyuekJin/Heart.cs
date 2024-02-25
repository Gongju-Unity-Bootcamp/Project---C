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
    private AudioSource audio;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
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
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            audio.Play();
            Invoke("Destroyed", 1f);
        }
    }

    private void Destroyed()
    {
        Destroy(gameObject);
    }

    
}
