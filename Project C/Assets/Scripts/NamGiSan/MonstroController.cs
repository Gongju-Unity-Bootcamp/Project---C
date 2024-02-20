using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private new Collider2D collider;
    public Animator animaotr;

    public Transform bulletPoint;
    public Transform bulletPoint2;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animaotr = GetComponent<Animator>();
    }


    void Start()
    {
        StartCoroutine(RandomState());
    }

    void FixedUpdate()
    {
        Flip();
    }

    void Flip()
    {
        if(player.GetComponent<Transform>().position.x > transform.position.x)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }

    IEnumerator RandomState()
    {
        yield return new WaitForSeconds(1f);

        int randomAction = Random.Range(0, 2);

        if (player != null && Player_Move.gameState != "gameover")
        {
            switch (randomAction)
            {
                case 0:
                    StartCoroutine(jumpReady());
                    break;
                case 1:
                    StartCoroutine(attackReady());
                    break;
            }
        }
        else
        {
            collider.enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator jumpReady()
    {
        animaotr.SetTrigger("JumpReady");
        yield return new WaitForSeconds(0.75f);

        int randomJump = Random.Range(0, 2);

        if (player != null && Player_Move.gameState != "gameover")
        {
            switch (randomJump)
            {
                case 0:
                    StartCoroutine(Chase());
                    break;
                case 1:
                    StartCoroutine(DiveAttack());
                    break;
            }
        }

        else
        {
            collider.enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator Chase()
    {
        animaotr.SetTrigger("Chase");
        Vector2 startPos = transform.position;
        Vector2 targetPos = player.transform.position;
        collider.enabled = false;

        float moveTime = 1f;
        float elapsedTime = 0f;

        while(elapsedTime < moveTime)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        animaotr.SetTrigger("ChaseEnd");
        transform.position = targetPos;
        rb.velocity = Vector2.zero;
        collider.enabled = true;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomState());
    }

    IEnumerator DiveAttack()
    {
        animaotr.SetTrigger("HighJump");
        Vector2 targetPos = player.transform.position;
        float posY = transform.position.y;
        collider.enabled = false;
        rb.velocity = new Vector2(0, 30f);

        while(true)
        {
            if (transform.position.y >= posY + 40)
            {
                animaotr.SetTrigger("Dive");
                rb.velocity = Vector2.zero;
                break;
            }
            yield return null;  // 업데이트 전 무조건 실행, 계속 상승 방지
        }

        int spawnBullet = Random.Range(30, 35);

        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos - new Vector2(0, 0), Time.deltaTime * 7);
            if (transform.position.y < targetPos.y + 0.05)
            {
                animaotr.SetTrigger("Crash");
                collider.enabled = true;
                rb.velocity = Vector2.zero;
                transform.position = targetPos;
                for (int i = 0; i < spawnBullet; i++)
                {
                    int bulletSpeed = Random.Range(6, 8);
                    GameObject bossBullet = Boss_ObjectPooling.instance.GetBulletPool();
                    bossBullet.transform.position = bulletPoint2.position;
                    Rigidbody2D rb = bossBullet.GetComponent<Rigidbody2D>();
                    Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                    rb.AddForce(ranVec * bulletSpeed, ForceMode2D.Impulse);
                }
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomState());
    }

    IEnumerator attackReady()
    {
        animaotr.SetTrigger("SpitReady");
        yield return new WaitForSeconds(1.0f);

        int randomJump = Random.Range(0, 2);

        if (player != null && Player_Move.gameState != "gameover")
        {
            switch (randomJump)
            {
                case 0:
                    StartCoroutine(SpitAttack());
                    break;
                case 1:
                    animaotr.SetTrigger("Idle");
                    StartCoroutine(RandomState());
                    break;
            }
        }

        else
        {
            collider.enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator SpitAttack()
    {
        animaotr.SetTrigger("Spit");
        yield return new WaitForSeconds(1.0f);

        int spawnBullet = Random.Range(20, 25);

        for (int i = 0; i < spawnBullet; i++)
        {
            int bulletSpeed = Random.Range(4, 7);
            GameObject bossBullet = Boss_ObjectPooling.instance.GetBulletPool();
            bossBullet.transform.position = bulletPoint.position;
            Rigidbody2D rb = bossBullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = (player.transform.position - transform.position).normalized;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 0.5f));
            dirVec += ranVec;
            rb.AddForce(dirVec * bulletSpeed, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomState());
    }
}
