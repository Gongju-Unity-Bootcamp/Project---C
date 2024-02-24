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
    private Animator animator;
    public GameObject bullet;
    void Start()
    {
        AttakCollTime = 3f;
        bulletForce = 5f;
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
            timeSinceLastDirectionChange = 0f;
        }

        MoveRandomly();

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        bool noObstacleBetween = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Obstacle"));

        if (noObstacleBetween && Time.time - shootBulletTime > AttakCollTime && distanceToPlayer < 5f)
        {
            ShootBullet();
            shootBulletTime = Time.time;
        }
    }
    private void ShootBullet()
    {
        animator.SetTrigger("OnAttack");
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rightBullet_rb = Bullet.GetComponent<Rigidbody2D>();
        rightBullet_rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
    private void MoveRandomly()
    {
        animator.SetTrigger("OnMove");
        transform.Translate(currentDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
