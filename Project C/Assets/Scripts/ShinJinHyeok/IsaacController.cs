using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsaacController : MonoBehaviour
{
    Transform _head;
    Transform _body;
    Transform _hit;

    Animator _headAnimator;
    Animator _bodyAnimator;

    SpriteRenderer _hitSpriteRenderer;

    public Sprite _hitSprite;

    private void Awake()
    {
        _head = transform.Find("Head");
        _body = transform.Find("Body");
        _hit  = transform.Find("Hit");

        _headAnimator = _head.GetComponent<Animator>();
        _bodyAnimator = _body.GetComponent<Animator>();

        _hitSpriteRenderer = _hit.GetComponent<SpriteRenderer>();
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
            StartCoroutine(HitCoroutine());
        }
    }

    IEnumerator HitCoroutine()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _hit.gameObject.SetActive(true);

        _hitSpriteRenderer.sprite = _hitSprite;
        yield return new WaitForSeconds(0.5f);

        _head.gameObject.SetActive(true);
        _body.gameObject.SetActive(true);
        _hit.gameObject.SetActive(false);
    }
}