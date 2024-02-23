using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsaacController : MonoBehaviour
{
    public float BlinkDurationForHit = 0.2f;
    public float PickUpTime = 1.0f;
    public float StopMove = 0.05f;
    public float BulletSpeed = 9.0f;

    public float MoveSpeed = 5.0f;
    public float CoolTime = 0.5f;
    public float Range = 1.5f;

    public Sprite HitSprite;
    public Sprite PickUpSprite;
    public GameObject BulletPrefab; // 프리팹을 가져온다

    Transform _head;
    Transform _body;
    Transform _total;

    Animator _headAnimator;
    Animator _bodyAnimator;
    Animator _totalAnimator;

    SpriteRenderer _totalSpriteRenderer;

    Rigidbody2D _playerRbody;
    GameObject _playerBullet; // 받아올 게임 오브젝트
    Vector2 _moveDirection;

    int _hp = 3;
    float _horizontal;
    float _vertical;

    // 공격 중인 상태에서는 다시 공격하지 못하도록 한다
    // 쿨타임 기능 구현에 사용한다
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
    public void ShootBullet(Vector2 direction)
    {
        // 함수가 시작되면서 공격 상태를 true로 변환한다
        _isAttack = true;
        _playerBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        _playerBullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;

        DestroyBullet();

        // 공격 상태를 변환하는 구조를 사용하여 쿨타임을 구현하였다
        Invoke("AttackCoolTime", CoolTime);
    }
    void DestroyBullet()
    {
        // 설정한 시간 이후에 불렛 오브젝트를 파괴하여 사거리를 구현하였다
        // 총알이 파괴되기 전에 애니메이션을 재생하여야 한다
        // 생성(Instantiate) > 총알이 날아간다(velocity) > 파괴될 시점(사거리) > 총알 정지 후 애니메이션 재생 > 총알 오브젝트 파괴
        Destroy(_playerBullet, Range);
    }
    void AttackCoolTime()
    {
        // 공격을 하지 않는 상태로 변환한다
        _isAttack = false;
    }
}