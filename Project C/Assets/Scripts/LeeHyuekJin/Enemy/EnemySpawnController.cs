using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private MonsterFactory monsterFactory;
    public GameObject[] spawn;
    private Collider2D _collider2D;
    void Start()
    {
        monsterFactory = new MonsterFactory(spawn);
        _collider2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플러스 델타타임해서 0.5초뒤에나오게 
        if (collision.gameObject.CompareTag("Player"))
        {
            _collider2D.GetComponent<Collider2D>().enabled = false;
            Invoke("SpawnRandomMonsterPattern", 0.5f);
        }
    }
    void SpawnRandomMonsterPattern()
    {
        int randomPattern = Random.Range(0, spawn.Length);
        monsterFactory.SpawnMonsterPattern(randomPattern, transform.position);
    }
}

public class MonsterFactory : EnemySpawnController
{
    public MonsterFactory(GameObject[] spawn)
    {
        this.spawn = spawn;
    }

    public GameObject SpawnMonsterPattern(int patternIndex, Vector3 spawnPosition)
    {
        return Instantiate(spawn[patternIndex], spawnPosition, Quaternion.identity);
    }
}