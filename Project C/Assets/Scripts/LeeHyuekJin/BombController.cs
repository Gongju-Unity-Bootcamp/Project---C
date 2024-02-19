using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explosionRadius;
    public float detonationTime;
    private GameObject _player;
    private Player_Health _player_Health;
    void Start()
    {
        Invoke("Detonate", detonationTime);
        _player = GameObject.FindWithTag("Player");
        _player_Health = _player.GetComponent<Player_Health>();
    }

    private void Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Destroy(collider.gameObject);
            }
            if(collider.CompareTag("Enemy"))
            {

            }
            if(collider.CompareTag("Player"))
            {
                _player_Health.TakeDamage();
            }
        }

        // ÆøÅº ÆÄ±«
        Destroy(gameObject);
    }
}
