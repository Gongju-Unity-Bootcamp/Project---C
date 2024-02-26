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
    public void Init()
    {
        Debug.Log($"{name} Init");
        m_cameraPo = GameObject.Find("Main Camera").transform;
        m_doorCol = GetComponent<BoxCollider2D>();
        room = transform.parent.gameObject;
        _roomManager = room.GetComponent<RoomManager>();
        Transform subDoor_Transfrom = transform.Find("Doors");
        subDoor = subDoor_Transfrom.gameObject;

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
            collision.gameObject.transform.position += playerInPosition * 1.5f;
            //m_cameraPo.position = transform.parent.position + new Vector3(0, 0, -10);
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
