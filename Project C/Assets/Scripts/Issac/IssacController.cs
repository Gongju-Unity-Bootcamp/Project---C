using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public float BlinkDurationForHit = 0.3f; // 수정
    public float PickUpTime = 1.0f;
    public float StopMove = 0.2f;
    public float BulletSpeed = 9.0f;

    public Sprite HitSprite;
    public Sprite PickUpSprite;
    public GameObject BulletPrefab;
    public GameObject BombPrefab;
    public Transform _firePoint1; // 추가
    public Transform _firePoint2; // 추가

    Transform _head;
    Transform _body;
    Transform _total;
    Transform _pickup;

    Animator _headAnimator;
    Animator _bodyAnimator;
    Animator _totalAnimator;

    SpriteRenderer _totalSpriteRenderer;
    SpriteRenderer _pickupSpriteRenderer;

    Rigidbody2D _playerRbody;
    GameObject _playerBullet;
    Vector2 _moveDirection;

    float _horizontal;
    float _vertical;
    int _hp = 3;
    bool _isHit = false;
    bool _isAttack = false;
    bool _useFirstPoint = true;     // 추가
    bool _isOrderInLayer = false;   // 추가

    PlayerStats playerStats;
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _total = transform.Find("Total");
        _pickup = transform.Find("PickupItem");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();
        _totalAnimator = _total.GetComponent<Animator>();

        _totalSpriteRenderer = _total.GetComponent<SpriteRenderer>();
        _pickupSpriteRenderer = _pickup.GetComponent<SpriteRenderer>();

        _playerRbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _headAnimator.SetBool("Up", Input.GetKey(KeyCode.UpArrow));
        _headAnimator.SetBool("Down", Input.GetKey(KeyCode.DownArrow));
        _headAnimator.SetBool("Left", Input.GetKey(KeyCode.LeftArrow));
        _headAnimator.SetBool("Right", Input.GetKey(KeyCode.RightArrow));

        _bodyAnimator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        _bodyAnimator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

        PlayerMove();

        if (_isAttack == false)
        {
            AttackDirection();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseBomb();
        }
    }

    #region 피격, 사망, 아이템 픽업 상태 구현
    public void GetHit()
    {
        if (_isHit == false)
        {

            _isHit = true;

            playerStats.TakeDamage();

            if (_hp <= 0)
            {
                Dead();
            }
            else
            {
                StartCoroutine(GetHitCo());
            }
        }
    }
    IEnumerator GetHitCo()
    {
         _head.gameObject.SetActive(false);
         _body.gameObject.SetActive(false);
        _total.gameObject.SetActive(true);

        _totalSpriteRenderer.sprite = HitSprite;
        for (int counter = 1; counter <= 5; ++counter)
        {
            _totalSpriteRenderer.enabled = !_totalSpriteRenderer.enabled;
            yield return new WaitForSeconds(BlinkDurationForHit);
        }
        _totalSpriteRenderer.enabled = true;

         _head.gameObject.SetActive(true);
         _body.gameObject.SetActive(true);
        _total.gameObject.SetActive(false);
        _isHit = false;
    }
    public void Dead()
    {
        _playerRbody.velocity = Vector2.zero;
         _head.gameObject.SetActive(false);
         _body.gameObject.SetActive(false);

        _total.gameObject.SetActive(true);
        _totalAnimator.enabled = true;
        _totalAnimator.Play("Dead");
    }
    public void PickUpItem()
    {
        _isHit = true;
        StartCoroutine(PickUpCo());
    }
    IEnumerator PickUpCo()
    {
         _head.gameObject.SetActive(false);
         _body.gameObject.SetActive(false);
        _total.gameObject.SetActive(true);
       _pickup.gameObject.SetActive(true);

        _totalSpriteRenderer.sprite = PickUpSprite;
        yield return new WaitForSeconds(PickUpTime);

         _head.gameObject.SetActive(true);
         _body.gameObject.SetActive(true);
        _total.gameObject.SetActive(false);
       _pickup.gameObject.SetActive(false);
        _isHit = false;
    }
    #endregion
    public void UseBomb()
    {
        if (playerStats.bomb > 0)
        {
            playerStats.bomb--;
            Instantiate(BombPrefab, transform.position, Quaternion.identity);
            Debug.Log("남은 폭탄 개수 : " + playerStats.bomb);
        }
    }
    public void PlayerMove()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_hp > 0)
        {
            _moveDirection = new Vector2(_horizontal, _vertical);

            if (_moveDirection.sqrMagnitude > 1)
            {
                _moveDirection.Normalize();
            }

            if (_horizontal != 0 || _vertical != 0)
            {
                _playerRbody.velocity = Vector2.Lerp(_playerRbody.velocity, _moveDirection * playerStats.moveSpeed, 0.5f);
            }
            else
            {
                _playerRbody.velocity = Vector2.Lerp(_playerRbody.velocity, Vector2.zero, StopMove);
            }
        }
    }
    public void AttackDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            FrontBackBullet(Vector2.up);    // 수정
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            FrontBackBullet(Vector2.down);  // 수정
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ShootBullet(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ShootBullet(Vector2.left);
        }
    }
    public void ShootBullet(Vector2 direction) // 수정
    {
        _isAttack = true;

        int orderInLayer = _isOrderInLayer ? 5 : 3;
        _isOrderInLayer = !_isOrderInLayer;

        _playerBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        _playerBullet.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
        _playerBullet.GetComponent<PlayerBulletController>().attakDamage = playerStats.attackDamage;
        
        DestroyBullet();

        Invoke("AttackCoolTime", playerStats.attackDelayTime);
    }
    public void FrontBackBullet(Vector2 direction) // 수정
    {
        _isAttack = true;

        Transform selectedFirePoint = _useFirstPoint ? _firePoint1 : _firePoint2;
        _useFirstPoint = !_useFirstPoint;

        _playerBullet = Instantiate(BulletPrefab, selectedFirePoint.position, Quaternion.identity);

        if (direction == Vector2.up)
        {
            int orderInLayer = 3;
            _playerBullet.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        }

        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
        _playerBullet.GetComponent<PlayerBulletController>().attakDamage = playerStats.attackDamage;
        DestroyBullet();

        Invoke("AttackCoolTime", playerStats.attackDelayTime);
    }
    public void DestroyBullet()
    {
        // 애니메이션 재생 시간 0.5f
        StartCoroutine(DestroyBulletAnimation(_playerBullet, playerStats.bulletSurviveTime + 0.5f));
    }
    public void AttackCoolTime()
    {
        _isAttack = false;
    }
    IEnumerator DestroyBulletAnimation(GameObject bullet, float BulSurviveTime)
    {
        yield return new WaitForSeconds(BulSurviveTime - 0.5f);

        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bullet.GetComponent<Animator>().enabled = true;

            yield return new WaitForSeconds(0.5f);

            if (bullet != null)
            {
                Destroy(bullet);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Boss")) // 피격 태그에 보스 추가
        {
            GetHit();
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            _pickupSpriteRenderer.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            PickUpItem();
            Destroy(collision.gameObject);
        }
    }
}