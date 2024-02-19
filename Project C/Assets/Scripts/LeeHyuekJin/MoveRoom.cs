using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour
{
    BoxCollider2D m_doorCol;
    Transform m_cameraPo;

    public Vector3 playerInPosition;
    private GameObject navi;
    private NaviController _naviController;
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
    private void Start()
    {
        navi = GameObject.FindWithTag("GameController");
        _naviController = navi.GetComponent<NaviController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 문 충돌");
            collision.gameObject.transform.position += playerInPosition * 3;
            m_cameraPo.position = transform.parent.position + new Vector3(0, 0, -10);

            OpenDoor(collision.gameObject);
        }
    }

    private void OpenDoor(GameObject collision)
    {
        if (gameObject.name == "UpDoor")
        {
            _naviController.Scan(1);
        }
        else if (gameObject.name == "DownDoor")
        {
            _naviController.Scan(2);
        }
        else if (gameObject.name == "RightDoor")
        {
            _naviController.Scan(3);
        }
        else if (gameObject.name == "LeftDoor")
        {
            _naviController.Scan(4);
        }
    }
}
