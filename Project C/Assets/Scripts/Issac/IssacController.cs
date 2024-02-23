using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsaacController : MonoBehaviour
{
    public float BlinkDurationForHit = 0.2f;
    public float PickUpTime = 1.0f;
    public float StopMove = 0.05f;

    public float MoveSpeed = 6.0f;
    public float CoolTime = 0.4f;

    public Sprite HitSprite;
    public Sprite PickUpSprite;
    public GameObject BulletPrefab;

    Transform _head;
    Transform _body;
    Transform _total;

    Animator _headAnimator;
    Animator _bodyAnimator;
    Animator _totalAnimator;

    SpriteRenderer _totalSpriteRenderer;

    Rigidbody2D _playerRbody;
    Rigidbody2D _bulletRbody;
    GameObject _playerBullet;
    Vector2 _moveDirection;

    int _hp = 3;
    float _horizontal;
    float _vertical;
    bool _isAttack;

    void Awake()
    {
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _total = transform.Find("Total");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();
        _totalAnimator = _total.GetComponent<Animator>();

        _totalSpriteRenderer = _total.GetComponent<SpriteRenderer>();

        _playerRbody = GetComponent<Rigidbody2D>();
        _bulletRbody = BulletPrefab.GetComponent<Rigidbody2D>();
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
        AttackDirection();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetHit();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
    }

    #region 피격, 사망, 아이템 픽업 상태 구현
    bool _isHit = false;
    public void GetHit()
    {
        if (_isHit == false)
        {
            _isHit = true;

            _hp--;

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
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);

        _total.gameObject.SetActive(true);
        _totalAnimator.enabled = true;
        _totalAnimator.Play("Dead");
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

        _totalSpriteRenderer.sprite = PickUpSprite;
        yield return new WaitForSeconds(PickUpTime);

        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _total.gameObject.SetActive(false);
    }
    #endregion
    public void PlayerMove()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(_horizontal, _vertical);

        if (_moveDirection.sqrMagnitude > 1)
        {
            _moveDirection.Normalize();
        }
        
        if (_horizontal != 0 || _vertical != 0)
        {
            _playerRbody.velocity = Vector2.Lerp(_playerRbody.velocity, _moveDirection * MoveSpeed, 0.5f);
        }
        else
        {
            _playerRbody.velocity = Vector2.Lerp(_playerRbody.velocity, Vector2.zero, StopMove);
        }
    }
    public void AttackDirection()
    {
        if (_isAttack == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ShootBullet(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                ShootBullet(Vector2.down);
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
    }
    public void ShootBullet(Vector2 direction)
    {
            _isAttack = true;
            _playerBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * 8.0f;

            Invoke("AttackDelay", CoolTime);
    }
    void AttackDelay()
    {
        _isAttack = false;
        Destroy(_playerBullet);
    }
}