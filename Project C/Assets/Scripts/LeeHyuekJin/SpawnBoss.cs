using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    private bool _isSpawning = true;
    private Collider2D _collider2D;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isSpawning)
        {
            _collider2D.enabled = false;
            _isSpawning = false;
            Spawn();
        }
    }
    private void Spawn()
    {
        Instantiate(boss,transform.position, Quaternion.identity);
    }
}
