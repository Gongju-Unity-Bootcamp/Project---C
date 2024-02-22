using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour
{
    BoxCollider2D m_doorCol;
    Transform m_cameraPo;

    public Vector3 playerInPosition;
    private GameObject room;
    private RoomManager _roomManager;
    private GameObject subDoor;
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
        room = transform.parent.gameObject;
        _roomManager = room.GetComponent<RoomManager>();
        Transform subDoor_Transfrom = transform.GetChild(1);
        subDoor = subDoor_Transfrom.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position += playerInPosition * 3;
            m_cameraPo.position = transform.parent.position + new Vector3(0, 0, -10);
        }
    }


    private void Update()
    {
        if (RoomManager.enemyCount == 0)
        {
            subDoor.SetActive(false);
        }
        else
        {
            subDoor.SetActive(true);
        }
    }
}
