using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public float BlinkDurationForHit = 0.2f;
    public float PickUpTime = 1.0f;
    public float StopMove = 0.2f;
    public float BulletSpeed = 9.0f;

    public Sprite HitSprite;
    public Sprite PickUpSprite;
    public Sprite UpHeadSprite;
    public Sprite DownHeadSprite;
    public Sprite LeftHeadSprite;
    public Sprite RightHeadSprite;
    public Sprite DiceSprite;
    public GameObject BulletPrefab;
    public GameObject BombPrefab;
    public Transform _firePoint1;
    public Transform _firePoint2;

    Transform _head;
    Transform _body;
    Transform _total;
    Transform _pickup;

    Animator _headAnimator;
    Animator _bodyAnimator;
    Animator _totalAnimator;

    SpriteRenderer _headRenderer;
    SpriteRenderer _totalSpriteRenderer;
    SpriteRenderer _pickupSpriteRenderer;

    Rigidbody2D _playerRbody;
    GameObject _playerBullet;
    Vector2 _moveDirection;

    float _horizontal;
    float _vertical;
    bool _isHit = false;
    bool _isAttack = false;
    bool _useFirstPoint = true;
    bool _isOrderInLayer = false;

    PlayerStats playerStats;
    void Awake()
    {
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _total = transform.Find("Total");
        _pickup = transform.Find("PickupItem");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();
        _totalAnimator = _total.GetComponent<Animator>();

        _headRenderer = _head.GetComponent<SpriteRenderer>();
        _totalSpriteRenderer = _total.GetComponent<SpriteRenderer>();
        _pickupSpriteRenderer = _pickup.GetComponent<SpriteRenderer>();

        _playerRbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUpItem();
            // 주사위 사용으로 발생하는 리롤 효과 추가
        }
    }

    #region 피격, 사망, 아이템 픽업 상태 구현
    public void GetHit()
    {
        if (_isHit == false)
        {
            _isHit = true;

            playerStats.TakeDamage();

            if (playerStats.hp > 0)
            {
                Managers.Sound.EffectSoundChange("Sound_Player_Hit");
                StartCoroutine(GetHitCo());
            }
            else
            {
                Managers.Sound.EffectSoundChange("Sound_Player_Dead");
                Dead();
            }
        }
    }
    IEnumerator GetHitCo()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _total.gameObject.SetActive(true);

        _totalSpriteRenderer.sprite = HitSprite;
        for (int counter = 1; counter <= 10; ++counter)
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
        _playerRbody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);

        _total.gameObject.SetActive(true);
        _totalAnimator.enabled = true;
        _totalAnimator.Play("Dead");
        _isAttack = true;
        _isHit = true;
    }
    public void PickUpItem()
    {
        StartCoroutine(PickUpCo());
    }
    IEnumerator PickUpCo()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _total.gameObject.SetActive(true);
        _pickup.gameObject.SetActive(true);

        _totalSpriteRenderer.sprite = PickUpSprite;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 주사위 스프라이트를 머리 위에 띄움
            _pickupSpriteRenderer.sprite = DiceSprite;
        }
        yield return new WaitForSeconds(PickUpTime);

        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _total.gameObject.SetActive(false);
        _pickup.gameObject.SetActive(false);
    }
    #endregion
    public void UseBomb()
    {
        if (playerStats.bomb > 0)
        {
            playerStats.bomb--;
            Instantiate(BombPrefab, transform.position, Quaternion.identity);
        }
    }
    public void PlayerMove()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.DownArrow) == false &&
            Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
        {
            if (_vertical > 0)
            {
                _headAnimator.enabled = false;
                _headRenderer.sprite = UpHeadSprite;
            }
            else if (_horizontal < 0)
            {
                _headAnimator.enabled = false;
                _headRenderer.sprite = LeftHeadSprite;
            }
            else if (_horizontal > 0)
            {
                _headAnimator.enabled = false;
                _headRenderer.sprite = RightHeadSprite;
            }
            else
            {
                _headAnimator.enabled = false;
                _headRenderer.sprite = DownHeadSprite;
            }
        }
        else
        {
            _headAnimator.enabled = true;
        }

        if (playerStats.hp > 0)
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
            FrontBackBullet(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            FrontBackBullet(Vector2.down);
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
    public void ShootBullet(Vector2 direction)
    {
        _isAttack = true;
        Managers.Sound.EffectSoundChange("Sound_Player_AttackTears");

        int orderInLayer = _isOrderInLayer ? 5 : 3;
        _isOrderInLayer = !_isOrderInLayer;

        _playerBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        _playerBullet.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
        _playerBullet.GetComponent<PlayerBulletController>().attakDamage = playerStats.attackDamage;

        DestroyBullet();

        Invoke("AttackCoolTime", playerStats.attackDelayTime);
    }
    public void FrontBackBullet(Vector2 direction)
    {
        _isAttack = true;
        Managers.Sound.EffectSoundChange("Sound_Player_AttackTears");

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
        StartCoroutine(DestroyBulletAnimation(_playerBullet, playerStats.bulletSurviveTime + 0.5f));
    }
    public void AttackCoolTime()
    {
        _isAttack = false;
    }
    public IEnumerator DestroyBulletAnimation(GameObject bullet, float BulSurviveTime)
    {
        yield return new WaitForSeconds(BulSurviveTime - 0.6f);

        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody2D>().gravityScale = 5;
            yield return new WaitForSeconds(0.1f);

            if (bullet != null)
            {
                Managers.Sound.EffectSoundChange("TearImpacts2");

                bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                bullet.GetComponent<Animator>().enabled = true;

                yield return new WaitForSeconds(0.5f);

                if (bullet != null)
                {
                    Destroy(bullet);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Boss"))
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