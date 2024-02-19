using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] spawn;
    private Collider2D _collider2D;
    private bool _isSpawning = true;

    private GameObject navi;
    private NaviController _naviController;
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();

        navi = GameObject.FindWithTag("GameController");
        _naviController = navi.GetComponent<NaviController>();
        
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

    void SpawnRandomMonsterPattern()
    {
        int randomPattern = Random.Range(0, spawn.Length);
        SpawnMonsterPattern(randomPattern, transform.position);
        _naviController.Scan(0);
    }

    GameObject SpawnMonsterPattern(int patternIndex, Vector3 spawnPosition)
    {
        Debug.Log("½ºÆù");
        return Instantiate(spawn[patternIndex], spawnPosition, Quaternion.identity);
    }
}
