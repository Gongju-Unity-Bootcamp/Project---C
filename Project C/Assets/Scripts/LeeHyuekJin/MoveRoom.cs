using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour
{
    BoxCollider2D m_doorCol;

    public Transform playerInPosition;
    private GameObject subDoor;
    public void Init()
    {
        m_doorCol = GetComponent<BoxCollider2D>();

        playerInPosition = transform.Find("Position");

        Transform subDoor_Transfrom = transform.Find("Doors");
        subDoor = subDoor_Transfrom.gameObject;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerInPosition.position;
        }

    }


    private void Update()
    {
        if (subDoor != null)
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
}
