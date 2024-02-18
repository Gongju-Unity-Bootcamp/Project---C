using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonstroController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private new Collider2D collider;

    public Transform bulletPoint;
    public Transform bulletPoint2;
    private int direction;// 기본 이미지스프라이트가 왼쪽을 향함 

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }


    void Start()
    {
        StartCoroutine(RandomState());
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        direction = (player.GetComponent<Transform>().position.x < transform.position.x ? 1 : -1); 
        transform.localScale = new Vector3(direction, 1, 1);
    }

    IEnumerator RandomState()
    {
        // 기본 대기 동작
        yield return new WaitForSeconds(1f);

        int randomAction = Random.Range(0, 2);

        switch(randomAction)
        {
            case 0:
                StartCoroutine(jumpReady());
                break;
            case 1:
                StartCoroutine(attackReady());
                break;
        }
    }

    IEnumerator jumpReady()
    {
        //점프 대기 동작
        yield return new WaitForSeconds(0.5f);

        int randomJump = Random.Range(0, 2);

        switch(randomJump)
        {
            case 0:
                StartCoroutine(Chase());
                break;
            case 1:
                StartCoroutine(DiveAttack());
                break;
        }
    }

    IEnumerator Chase()
    {
        // 작은 점프 애니메이션
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

        transform.position = targetPos;
        collider.enabled = true;
        // 착지 애니메이션

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomState());
    }

    IEnumerator DiveAttack()
    {
        // 도약 애니메이션
        Vector2 targetPos = player.transform.position;
        int spawnBullet = Random.Range(30, 35);
        float posY = transform.position.y;
        collider.enabled = false;
        rb.velocity = new Vector2(0, 30f);

        while(true)
        {
            if (transform.position.y >= posY + 40)
            {
                // 강하 애니메이션
                rb.velocity = Vector2.zero;
                break;
            }
            yield return null;  // 업데이트 전 무조건 실행, 계속 상승 방지
        }

        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos - new Vector2(0, 0), Time.deltaTime * 7);
            if (transform.position.y < targetPos.y + 0.05)
            {
                // 착지 애니메이션
                collider.enabled = true;
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
        // 원거리 공격 대기 동작
        yield return new WaitForSeconds(0.5f);

        int randomJump = Random.Range(0, 2);

        switch (randomJump)
        {
            case 0:
                StartCoroutine(SpitAttack());
                break;
            case 1:
                StartCoroutine(RandomState());
                break;
        }
    }

    IEnumerator SpitAttack()
    {
        // 원거리 공격 자세
        yield return new WaitForSeconds(0.5f);

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

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomState());
    }
}
