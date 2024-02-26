using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorfController : MonoBehaviour
{
    private GameObject player;
    private float shootBulletTime;
    public GameObject bullet;
    private float bulletForce = 5f;
    private float AttakCollTime;
    private Animator _animator;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AttakCollTime = 1.5f;
        player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float yDifference = Mathf.Abs(player.transform.position.y - transform.position.y);
        float xDifference = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool noObstacleBetween = !Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Obstacle"));
        if (noObstacleBetween && Time.time - shootBulletTime > AttakCollTime && (Mathf.Approximately(yDifference, -3f) || yDifference < 3f))
        {
            StartCoroutine(ShootBullet());
            shootBulletTime = Time.time;
        }
        else if (noObstacleBetween && Time.time - shootBulletTime > AttakCollTime && (Mathf.Approximately(xDifference, -3f) || xDifference < 3f))
        {
            StartCoroutine(ShootBullet());
            shootBulletTime = Time.time;
        }
    }

    IEnumerator ShootBullet()
    {
        _animator.SetTrigger("OnAttak");
        yield return new WaitForSeconds(0.6f);
        _audioSource.Play();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rightBullet_rb = Bullet.GetComponent<Rigidbody2D>();
        rightBullet_rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    }
}
