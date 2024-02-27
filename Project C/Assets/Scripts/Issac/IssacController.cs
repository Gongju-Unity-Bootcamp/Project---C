using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
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

    public RuntimeAnimatorController _deadAnimController;
    public RuntimeAnimatorController _pickupAnimController;

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
        UpdateHeadAnimator();
        UpdateBodyAnimator();

        Move();

        Attack();

        UseBomb();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUpItem();
            // 주사위 사용으로 발생하는 리롤 효과 추가
        }
    }
    public void UpdateHeadAnimator()
    {
        _headAnimator.SetBool("Up", Input.GetKey(KeyCode.UpArrow));
        _headAnimator.SetBool("Down", Input.GetKey(KeyCode.DownArrow));
        _headAnimator.SetBool("Left", Input.GetKey(KeyCode.LeftArrow));
        _headAnimator.SetBool("Right", Input.GetKey(KeyCode.RightArrow));
    }
    public void UpdateBodyAnimator()
    {
        _bodyAnimator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        _bodyAnimator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
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
            yield return new WaitForSeconds(0.2f);
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

        _totalAnimator.runtimeAnimatorController = _deadAnimController;
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

        PickUpAnim();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pickupSpriteRenderer.sprite = DiceSprite;
        }
        yield return new WaitForSeconds(1.0f);
        _totalAnimator.enabled = false;

        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _total.gameObject.SetActive(false);
        _pickup.gameObject.SetActive(false);
    }
    public void PickUpAnim()
    {
        _totalAnimator.enabled = true;
        _totalAnimator.runtimeAnimatorController = _pickupAnimController;
        _totalAnimator.Play("Pickup");
    }
    #endregion
    public void UseBomb()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Bomb();
        }
    }
    public void Bomb()
    {
        if (playerStats.bomb > 0)
        {
            playerStats.bomb--;
            Instantiate(BombPrefab, transform.position, Quaternion.identity);
        }
    }
    public void Move()
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
                _playerRbody.velocity = Vector2.Lerp(_playerRbody.velocity, Vector2.zero, 0.2f);
            }
        }
    }
    public void Attack()
    {
        if (_isAttack == false)
        {
            AttackDirection();
        }
    }
    public void AttackDirection()
    {
        Vector2 ranVec = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 playerPosition = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float deltaX = mousePosition.x - playerPosition.x;
            float deltaY = mousePosition.y - playerPosition.y;

            bool isHorizontal = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

            Vector2 shootDirection = isHorizontal ? (deltaX > 0 ? Vector2.right : Vector2.left) : (deltaY > 0 ? Vector2.up : Vector2.down);

            FrontBackBullet(shootDirection);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            FrontBackBullet(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            FrontBackBullet(Vector2.down + ranVec);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            LeftRightBullet(Vector2.right + ranVec);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRightBullet(Vector2.left + ranVec);
        }
    }
    public void LeftRightBullet(Vector2 direction)
    {
        _isAttack = true;
        Managers.Sound.EffectSoundChange("Sound_Player_AttackTears");

        int orderInLayer = _isOrderInLayer ? 6 : 3;
        _isOrderInLayer = !_isOrderInLayer;

        _playerBullet = PlayerBulletPool.instance.Pool.Get();
        _playerBullet.transform.position = _head.transform.position;

        _playerBullet.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;

        Invoke("AttackCoolTime", playerStats.attackDelayTime);
    }
    public void FrontBackBullet(Vector2 direction)
    {
        _isAttack = true;
        Managers.Sound.EffectSoundChange("Sound_Player_AttackTears");

        int orderInLayer;
        Transform selectedFirePoint = _useFirstPoint ? _firePoint1 : _firePoint2;
        _useFirstPoint = !_useFirstPoint;

        _playerBullet = PlayerBulletPool.instance.Pool.Get();
        _playerBullet.transform.position = selectedFirePoint.position;

        if (direction == Vector2.up)
        {
            orderInLayer = 3;
        }
        else
        {
            orderInLayer = 6;
        }
        _playerBullet.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;

        Invoke("AttackCoolTime", playerStats.attackDelayTime);
    }
    public void AttackCoolTime()
    {
        _isAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Boss"))
        {
            GetHit();
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            Item go = collision.transform.GetComponent<Item>();
            if (go.itemType == ItemType.Active || go.itemType == ItemType.Passive)
            {
                _pickupSpriteRenderer.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                PickUpItem();
            }
            Destroy(collision.gameObject);
        }
    }
}