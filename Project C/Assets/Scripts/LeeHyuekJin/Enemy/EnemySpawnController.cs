using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private MonsterFactory monsterFactory;
    public GameObject[] spawn;
    private Collider2D _collider2D;
    private bool _isSpawning = true;

    void Start()
    {
        monsterFactory = gameObject.AddComponent<MonsterFactory>();
        monsterFactory.Initialize(spawn);
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isSpawning)
        {
            _collider2D.enabled = false;
            Invoke("SpawnRandomMonsterPattern", 0.2f);
            _isSpawning = false;
        }
    }

    void SpawnRandomMonsterPattern()
    {
        int randomPattern = Random.Range(0, spawn.Length);
        monsterFactory.SpawnMonsterPattern(randomPattern, transform.position);
    }
}

public class MonsterFactory : MonoBehaviour
{
    private GameObject[] spawn;

    public void Initialize(GameObject[] spawn)
    {
        this.spawn = spawn;
    }

    public GameObject SpawnMonsterPattern(int patternIndex, Vector3 spawnPosition)
    {
        Debug.Log("½ºÆù");
        return Instantiate(spawn[patternIndex], spawnPosition, Quaternion.identity);
    }
}
