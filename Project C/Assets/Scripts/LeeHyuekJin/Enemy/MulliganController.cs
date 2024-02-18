using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MulliganController : MonoBehaviour
{
    
    public float moveSpeed;
    public GameObject bullet;
    public float bulletForce;

    public GameObject fly;
    private GameObject player;
    private Vector2 currentDirection;
    private float timeSinceLastDirectionChange;
    private float skillCollTime;
    private float shootBulletTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (timeSinceLastDirectionChange >= 2f)
        {
            if (distanceToPlayer < 3f)
            {
                RunAwayPlayer();
                timeSinceLastDirectionChange = 0;
            }
            else
            {
                MoveRandomly();
                timeSinceLastDirectionChange = 0;
            }
        }
        transform.Translate(currentDirection.normalized * moveSpeed * Time.deltaTime);

        float yDifference = Mathf.Abs(player.transform.position.y - transform.position.y);
        float xDifference = Mathf.Abs(player.transform.position.x - transform.position.x);

        bool noObstacleBetween = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Obstacle"));
        if (noObstacleBetween && Time.time - shootBulletTime > skillCollTime && (Mathf.Approximately(yDifference, 0.0f) || yDifference < 0.01f))
        {
            shootBulletTime = Time.time;
            ShootBullet();
        }
        else if (noObstacleBetween && Time.time - shootBulletTime > skillCollTime && (Mathf.Approximately(xDifference, 0.0f) || xDifference < 0.01f))
        {
            shootBulletTime = Time.time;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D Bullet_rb = Bullet.GetComponent<Rigidbody2D>();
        Bullet_rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
    void RunAwayPlayer()
    {
        currentDirection = (transform.position - player.transform.position).normalized;
    }
    void MoveRandomly()
    {
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            Invoke("CreateFlyEnemy", 0.01f);
        }
    }

    private void CreateFlyEnemy()
    {
        Instantiate(fly, transform.position, Quaternion.identity);
        Instantiate(fly, transform.position + new Vector3(0.1f, 0.1f, 0), Quaternion.identity);
    }
}
