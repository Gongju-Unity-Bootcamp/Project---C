using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
    Transform _head;
    Transform _body;
    Transform _hit;
    Transform _dead;
    Transform _pickUp;
    Transform _getItem;

    Animator _headAnimator;
    Animator _bodyAnimator;

    SpriteRenderer _hitSpriteRenderer;
    SpriteRenderer _pickUpSpriteRenderer;
    SpriteRenderer _getItemSpriteRenderer;

    Rigidbody2D _isaacRbody;

    public Sprite _getItemSprite;

    int _hp = 3;
    float _moveSpeed = 6;
    private void Awake()
    {
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _hit  = transform.Find("Hit");
        _dead = transform.Find("Dead");
        _pickUp = transform.Find("PickUp");
        _getItem = transform.Find("GetItem");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();

        _hitSpriteRenderer = _hit.GetComponent<SpriteRenderer>();
        _pickUpSpriteRenderer = _pickUp.GetComponent<SpriteRenderer>();
        _getItemSpriteRenderer = _getItem.GetComponent<SpriteRenderer>();

        _isaacRbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _headAnimator.SetBool("Up", Input.GetKey(KeyCode.UpArrow));
        _headAnimator.SetBool("Down", Input.GetKey(KeyCode.DownArrow));
        _headAnimator.SetBool("Left", Input.GetKey(KeyCode.LeftArrow));
        _headAnimator.SetBool("Right",Input.GetKey(KeyCode.RightArrow));

        _bodyAnimator.SetFloat("Horizontal", h);
        _bodyAnimator.SetFloat("Vertical", v);

        Vector2 direction = new Vector2(h, v);

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        // 미끄러지는 움직임 구현
        if (h != 0 || v != 0)
        {
            _isaacRbody.velocity = Vector2.Lerp(_isaacRbody.velocity, direction * _moveSpeed, 0.5f);
        }
        else
        {
            _isaacRbody.velocity = Vector2.Lerp(_isaacRbody.velocity, Vector2.zero, 0.05f);
        }

        // 피격 기능 호출
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_hp <= 1)
            {
                _head.gameObject.SetActive(false);
                _body.gameObject.SetActive(false);
                _hit.gameObject.SetActive(false);
                _dead.gameObject.SetActive(true);
                _pickUp.gameObject.SetActive(false);
                _getItem.gameObject.SetActive(false);

                return;
            }
            else
            {
                _hp--;
                Debug.Log("체력 : " + _hp);

                StartCoroutine(DamagedCoroutine());
            }
        }
        // 아이템 픽업 기능 호출
        if (Input.GetKeyDown(KeyCode.F))
        {
            Damage();
        }
    }
    void Damage()
    {
        StartCoroutine(PickUpItemCoroutine());
    }
    // 피격 상태
    IEnumerator DamagedCoroutine()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _hit.gameObject.SetActive(true);
        _dead.gameObject.SetActive(false);
        _pickUp.gameObject.SetActive(false);
        _getItem.gameObject.SetActive(false);
        #region 스프라이트 깜빡임
        yield return new WaitForSeconds(0.2f);
        _hitSpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        _hitSpriteRenderer.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        _hitSpriteRenderer.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        _hitSpriteRenderer.GetComponent<SpriteRenderer>().enabled = true;
        #endregion
        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _hit.gameObject.SetActive(false);
        _dead.gameObject.SetActive(false);
        _pickUp.gameObject.SetActive(false);
        _getItem.gameObject.SetActive(false);
    }
    // 아이템 픽업 상태
    IEnumerator PickUpItemCoroutine()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _hit.gameObject.SetActive(false);
        _dead.gameObject.SetActive(false);
        _pickUp.gameObject.SetActive(true);
        _getItem.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _hit.gameObject.SetActive(false);
        _dead.gameObject.SetActive(false);
        _pickUp.gameObject.SetActive(false);
        _getItem.gameObject.SetActive(false);
    }
}