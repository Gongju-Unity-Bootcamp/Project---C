using Maps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// ĳ���� �̵��� �ǰ� �� ���� �ð� ���� ����� ������ ��ũ��Ʈ
public class Player_Move : Player_Health
{
    private PlayerStats playerstats;
    private Rigidbody2D playerRbody;
    public GameObject playerHp;
    public GameObject[] m_Hp;
    public Transform m_HpController;

    float _axisHor;
    float _axisVer;
    float _acceleration = 0.5f;
    float _deceleration = 0.1f;
    float _invincibleTime = 1.5f;   // ���� �ð�
    public static string gameState;
    public static bool isDamage = false;
    Test_Inventory testinentory;
    
    // ����
    public static float moveSpeed { get; set; }
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
        // ���� �ð� ���濡 ����� ��������Ʈ ��Ȱ��ȭ
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = true;
        }

        isInvincible = false;
        isDamage = false;
    }

    private void Awake()
    {

        // GameObject go = new GameObject(nameof(Test_Inventory));
        // go.transform.parent = transform;

        testinentory = GameObject.FindObjectOfType<Test_Inventory>();
        for (int i = 0; i < m_Hp.Length; ++i)
        {
            GameObject hp = m_HpController.transform.Find($"Life ({i})").gameObject;
            m_Hp[i] = hp;
        }
    }


    void Start()
    {
        playerRbody = GetComponent<Rigidbody2D>();
        playerstats = GetComponent<PlayerStats>();
        
        gameState = "playing";
    }
    private void Update()
    {
        moveSpeed = playerstats.moveSpeed;
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
        // ü�� ������Ʈ �ʿ���
        if (hp > 0)
        {
            Test_TakeDamager();
            Debug.Log("���� ü�� : " + hp);
            StartCoroutine(NoDamage(_invincibleTime, 0.2f));
        }
        else
        {
            m_Hp[hp].SetActive(false);
            Player_GameManager.instance.GameOver();
        }
    }
    public void Test_TakeDamager()
    {
        //m_Hp[hp].SetActive(false);
        //hp--;

        testinentory.Test_TakeDamager();
    }
}