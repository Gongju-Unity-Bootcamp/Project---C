using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] spawn;
    private Collider2D _collider2D;
    private bool _isSpawning = true;

    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isSpawning)
        {
            _collider2D.enabled = false;
            SpawnRandomMonsterPattern();
            _isSpawning = false;
        }
    }
    private void SpawnRandomMonsterPattern()
    {
        int randomPattern = Random.Range(0, spawn.Length);
        Instantiate(spawn[randomPattern], transform.position, Quaternion.identity);
    }
}
