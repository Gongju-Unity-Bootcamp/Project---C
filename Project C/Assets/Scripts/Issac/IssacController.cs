using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public float BlinkDurationForHit = 0.2f;
    public float PickUpTime = 1.0f;

    public Sprite HitSprite;
    public Sprite PickUpSprite;

    Transform _head;
    Transform _body;
    Transform _total;

    Animator _headAnimator;
    Animator _bodyAnimator;
    Animator _totalAnimator;

    SpriteRenderer _totalSpriteRenderer;
    int _hp = 3;

    void Awake()
    {
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _total = transform.Find("Total");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();
        _totalAnimator = _total.GetComponent<Animator>();

        _totalSpriteRenderer = _total.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        _headAnimator.SetBool("Up", Input.GetKey(KeyCode.UpArrow));
        _headAnimator.SetBool("Down", Input.GetKey(KeyCode.DownArrow));
        _headAnimator.SetBool("Left", Input.GetKey(KeyCode.LeftArrow));
        _headAnimator.SetBool("Right", Input.GetKey(KeyCode.RightArrow));

        _bodyAnimator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        _bodyAnimator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetHit();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
    }


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
}