using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class MulliganController : MonoBehaviour
{
    
    public float moveSpeed;
    public GameObject bullet;
    public float bulletForce;

    public GameObject fly;
    private GameObject player;
    private Rigidbody2D _rb;
    private Vector2 currentDirection;
    private float timeSinceLastDirectionChange;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        _rb.velocity = currentDirection.normalized * moveSpeed;
        if (_rb.velocity.x > 0 || _rb.velocity.x < 0)
        {
            if(_rb.velocity.x != 0)
            {
                if(_rb.velocity.x > 0)
                {
                    _animator.SetTrigger("MoveRight");
                }
                else
                {
                    _animator.SetTrigger("MoveLeft");
                }
            }
        }
    }

    void ShootBullet()
    {

        GameObject upBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D upBullet_rb = upBullet.GetComponent<Rigidbody2D>();
        upBullet_rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        GameObject downBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D downBullet_rb = downBullet.GetComponent<Rigidbody2D>();
        downBullet_rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);

        GameObject rightBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rightBullet_rb = rightBullet.GetComponent<Rigidbody2D>();
        rightBullet_rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);

        GameObject leftBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D leftBullet_rb = leftBullet.GetComponent<Rigidbody2D>();
        leftBullet_rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
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
            ShootBullet();
            CreateFlyEnemy();
        }
    }

    private void CreateFlyEnemy()
    {
        Instantiate(fly, transform.position, Quaternion.identity);
        Instantiate(fly, transform.position + new Vector3(0.1f, 0.1f, 0), Quaternion.identity);
        Instantiate(fly, transform.position + new Vector3(-0.1f, -0.1f, 0), Quaternion.identity);
    }
}
