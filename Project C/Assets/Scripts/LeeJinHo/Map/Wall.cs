using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoxCollider2D m_wallCollider;
    private Rigidbody2D m_body2D;
    private bool isDoor = true;
    float m_time = 0;
    string doorName;
    private void Awake()
    {
        m_wallCollider = GetComponent<BoxCollider2D>();
        m_body2D = GetComponent<Rigidbody2D>();
        Init();
    }

    private void Init()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.childCount == 0)
        {
            if (collision.gameObject.CompareTag("BossWall"))
            {
                Managers.Spawn.CreatDoor(gameObject.name, transform);

            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                if (transform.childCount == 0)
                {
                    gameObject.name = gameObject.name switch
                    {
                        "UpDoor" => "UpDoor",
                        "DownDoor" => "DownDoor",
                        "RightDoor" => "RightDoor",
                        "LeftDoor" => "LeftDoor",
                        "BossUpDoor" => "UpDoor",
                        "BossDownDoor" => "DownDoor",
                        "BossRightDoor" => "RightDoor",
                        "BossLeftDoor" => "LeftDoor"
                    };
                    Managers.Spawn.CreatDoor(name, transform);
                }
            }
        }
        
    }
}