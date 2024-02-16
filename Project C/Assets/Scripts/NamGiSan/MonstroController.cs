using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonstroController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Collider2D collider;

    public Transform bulletPoint;

    private float bulletSpeed = 4f;
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
        yield return new WaitForSeconds(1f);

        int randomAction = Random.Range(0, 1);

        switch(randomAction)
        {
            case 0:
                StartCoroutine(jumpReady());
                break;
            case 1:
                StartCoroutine(attackReady());
                break;
            case 2:
                break;
            default: 
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
            default : 
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
        collider.enabled = false;

        


        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomState());
    }

    IEnumerator attackReady()
    {
        yield return new WaitForSeconds(1f);
    }
}
