using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    GameObject _playerBullet;
    void Start()
    {
        _playerBullet = this.gameObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerBullet != null)
        {
            StartCoroutine(CollisionBullet(_playerBullet));
        }
    }
    IEnumerator CollisionBullet(GameObject bullet)
    {
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
}