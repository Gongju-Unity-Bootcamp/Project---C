using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ĳ���� �̵��� �ǰ� �� ���� �ð� ���� ����� ������ ��ũ��Ʈ
public class Player_Move : Player_Health
{
    private Rigidbody2D playerRbody;

    float _axisHor;
    float _axisVer;
    float _acceleration = 0.5f;
    float _deceleration = 0.1f;
    float _invincibleTime = 1.5f;   // ���� �ð�
    public static string gameState;
    bool isDamage = false;
    // ����
    public static float moveSpeed = 3.0f;
    Player_Move()
    {
        hp = 7;
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

            // �ڽ� ������Ʈ�� ��������Ʈ ������
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
        // �̲������� ������ ����
        if (_axisHor != 0 || _axisVer != 0)
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, move * moveSpeed, _acceleration);
        }
        else
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, Vector2.zero, _deceleration);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet")) && gameState == "playing")
        {
            TakeDamage();
        }
    }
    public override void TakeDamage()
    {
        if (isInvincible)
        {
            return;
        }

        isDamage = true;

        if (hp > 1)
        {
            hp--;
            Debug.Log("���� ü�� : " + hp);

            StartCoroutine(NoDamage(_invincibleTime, 0.2f));
        }
        else
        {
            Player_GameManager.instance.GameOver();
        }
    }
}