using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Move : Player_Health
{
    Rigidbody2D playerRbody;

    float _axisHor;
    float _axisVer;
    float _acceleration = 0.5f;
    float _deceleration = 0.1f;
    public static string gameState;
    bool isDamage = false;
    // 스탯
    float moveSpeed = 5.0f;
    Player_Move()
    {
        hp = 8;
    }

    void Start()
    {
        playerRbody = GetComponent<Rigidbody2D>();
        gameState = "playing";

    }
    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        // 피격시 이미지 깜빡임
        if (isDamage)
        {
            float val = Mathf.Sin(Time.time * 30);

            if (val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        _axisHor = Input.GetAxisRaw("Horizontal");
        _axisVer = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(_axisHor, _axisVer);

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }
        // 미끄러지는 움직임 구현
        if (_axisHor != 0 || _axisVer != 0)
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, move * moveSpeed, _acceleration);
        }
        else
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, Vector2.zero, _deceleration);
        }
    }
    public override void TakeDamage(GameObject enemy)
    {
        if (hp > 0)
        {
            hp--;
            Debug.Log("현재 체력 : " + hp);
            GetComponent<CircleCollider2D>().enabled = false;

            isDamage = true;
            Invoke("DamageEnd", 3.0f);
        }
        else
        {
            Player_GameManager.instance.GameOver();
        }
    }
    void DamageEnd()
    {
        isDamage = false;
        GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(other.gameObject);
        }
    }
}