using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_Move : Player_Health
{
    Rigidbody2D playerRbody;

    float _axisHor;
    float _axisVer;
    float _acceleration = 0.5f;
    float _deceleration = 0.1f;
    float _invincibleTime = 1.5f;   // 무적 시간
    public static string gameState;
    bool isDamage = false;
    // 스탯
    float moveSpeed = 5.0f;

    
    #region
    Test_Player_Move()
    {
        hp = 8;
        isInvincible = false;
    }
    public IEnumerator NoDamage(float blinkTime, float intervalTime)
    {
        isInvincible = true;

        float startTime = Time.time;
        while (Time.time < startTime + blinkTime)
        {
            bool isSpriteEnabled = !GetComponent<SpriteRenderer>().enabled;
            GetComponent<SpriteRenderer>().enabled = isSpriteEnabled;

            // 자식 오브젝트의 스프라이트 렌더러
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.enabled = isSpriteEnabled;
            }
            yield return new WaitForSeconds(intervalTime);
        }

        GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = true;
        }

        isInvincible = false;
        isDamage = false;
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

    #endregion
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameState == "playing")
        {
            TakeDamage();
        }

        if(other.gameObject.CompareTag("Item"))
        {
            
        }
    }
    public override void TakeDamage()
    {
        if (isInvincible)
        {
            return;
        }

        isDamage = true;

        if (hp > 0)
        {
            hp--;
            Debug.Log("현재 체력 : " + hp);

            StartCoroutine(NoDamage(_invincibleTime, 0.2f));
        }
        else
        {
            Player_GameManager.instance.GameOver();
        }
    }

    
}