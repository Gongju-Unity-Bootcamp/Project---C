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
            SpawnRandomMonsterPattern();
        }
    }
    void SpawnRandomMonsterPattern()
    {
        int randomPattern = Random.Range(1, 4);

        switch (randomPattern)
        {
            case 1:
                monsterFactory.SpawnMonsterPattern1(transform.position);
                break;
            case 2:
                monsterFactory.SpawnMonsterPattern2(transform.position);
                break;
            case 3:
                monsterFactory.SpawnMonsterPattern3(transform.position);
                break;
        }
    }
}

public class MonsterFactory : EnemySpawnController
{
    public MonsterFactory(GameObject[] spawn)
    {
        this.spawn = spawn;
    }
    public GameObject SpawnMonsterPattern1(Vector3 spawnPosition)
    {
        return Instantiate(spawn[0], spawnPosition, Quaternion.identity);
    }
    public GameObject SpawnMonsterPattern2(Vector3 spawnPosition)
    {
        return Instantiate(spawn[1], spawnPosition, Quaternion.identity);
    }
    public GameObject SpawnMonsterPattern3(Vector3 spawnPosition)
    {
        return Instantiate(spawn[2], spawnPosition, Quaternion.identity);
    }
}