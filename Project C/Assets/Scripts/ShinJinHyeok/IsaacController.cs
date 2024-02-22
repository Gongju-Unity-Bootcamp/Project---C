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

    public Sprite _getItemSprite;
    int _hp = 7;
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
                Debug.Log("Ã¼·Â : " + _hp);

                StartCoroutine(DamagedCoroutine());
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PickUpItemCoroutine());
        }
    }

    IEnumerator DamagedCoroutine()
    {
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        _hit.gameObject.SetActive(true);
        _dead.gameObject.SetActive(false);
        _pickUp.gameObject.SetActive(false);
        _getItem.gameObject.SetActive(false);
        #region ½ºÇÁ¶óÀÌÆ® ±ôºýÀÓ
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