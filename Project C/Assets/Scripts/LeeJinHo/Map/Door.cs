using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;


public class Doors : MonoBehaviour
{
    BoxCollider2D m_doorCol;
    Transform m_cameraPo;

    public Vector3 playerInPosition;

    void Awake()
    {
        m_cameraPo = GameObject.Find("Main Camera").transform;
        m_doorCol = GetComponent<BoxCollider2D>();

        switch (gameObject.name)
        {
            case "LeftDoor":
                playerInPosition = Vector3.left;
                break;
            case "RightDoor":
                playerInPosition = Vector3.right;
                break;
            case "UpDoor":
                playerInPosition = Vector3.up;
                break;
            case "DownDoor":
                playerInPosition = Vector3.down;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position += playerInPosition * 3f;
            m_cameraPo.position = transform.parent.position + new Vector3(0, 0, -10);
        }
    }
}

