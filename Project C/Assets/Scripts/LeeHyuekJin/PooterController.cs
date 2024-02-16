using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooterController : MonoBehaviour
{
    public float moveSpeed;
    private float changeDirectionInterval = 2f;
    private float timeSinceLastDirectionChange = 0f;
    private float bulletForce;
    private float shootBulletTime;
    private float AttakCollTime;

    private Vector2 currentDirection;
   
    private GameObject player;
    public GameObject bullet;
    void Start()
    {
        AttakCollTime = 1.5f;
        bulletForce = 5f;
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {

        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
            timeSinceLastDirectionChange = 0f;
        }

        MoveRandomly();

        float yDifference = Mathf.Abs(player.transform.position.y - transform.position.y);
        float xDifference = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool noObstacleBetween = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Obstacle"));
        if (noObstacleBetween && Time.time - shootBulletTime > AttakCollTime && (Mathf.Approximately(yDifference, -5f) || yDifference < 5f))
        {
            ShootBullet();
            shootBulletTime = Time.time;
        }
        else if (noObstacleBetween && Time.time - shootBulletTime > AttakCollTime && (Mathf.Approximately(xDifference, -5f) || xDifference < 5f))
        {
            ShootBullet();
            shootBulletTime = Time.time;
        }
    }
    private void ShootBullet()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rightBullet_rb = Bullet.GetComponent<Rigidbody2D>();
        rightBullet_rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
    void MoveRandomly()
    {
        transform.Translate(currentDirection.normalized * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
