using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RandomPosition
{
    Left,
    Right,
    Up,
    Down
}
public class MainMap : MonoBehaviour
{
    [SerializeField] private GameObject[] m_rooms;
    [SerializeField] private Queue<GameObject> m_bossRooms;
    [SerializeField] private Transform m_testRoom;
    [SerializeField] private MiniMap m_miniMap;
    private RandomPosition m_randomPosition;

    private List<Vector3> m_testPosition;

    bool m_isRoomCheck;

    private void Awake()
    {
        m_testPosition = new List<Vector3>();
        m_rooms = new GameObject[transform.childCount];
        m_testRoom = transform.Find("TestRoom");
        m_testPosition.Add(m_testRoom.position);
    }

    private void Start()
    {
        Init();
    }


    private int m_Count = 0;
    void Init()
    {
        foreach(Transform tile in transform)
        { 
            Transform m_room = transform.Find($"BagicRoom ({m_Count})");
            
            if (m_room == null) { continue; }

            m_rooms[m_Count] = m_room.gameObject;
            RoomSetting(m_rooms[m_Count]);
            m_Count += 1;
        }

        for (int i = 0; i < m_rooms.Length; i++)
        {
            GameObject go = transform.Find($"BossRoom ({i})").gameObject;
            m_bossRooms.Enqueue(go);
            BossRoomSetting(m_bossRooms.Dequeue());
        }

    }

    void RoomSetting(GameObject room)
    {
        Vector3 rePosition = Vector3.zero;
        int m_mapX = 18;
        int m_mapY = 10;

        m_randomPosition = (RandomPosition)Random.Range(0, 4);
        switch (m_randomPosition)
        {
            case RandomPosition.Left:
                rePosition = new Vector3(m_testRoom.position.x - m_mapX, m_testRoom.position.y, 0);
                break;
            case RandomPosition.Right:
                rePosition = new Vector3(m_testRoom.position.x + m_mapX, m_testRoom.position.y, 0);
                break;
            case RandomPosition.Up:
                rePosition = new Vector3(m_testRoom.position.x, m_testRoom.position.y + m_mapY, 0);
                break;
            case RandomPosition.Down:
                rePosition = new Vector3(m_testRoom.position.x, m_testRoom.position.y - m_mapY, 0);
                break;
        }
        m_testRoom.position = rePosition;


        foreach(Vector3 vector in m_testPosition) 
        { 
            if(vector == m_testRoom.position)
            {
                m_isRoomCheck = true;
            }
        }

        if (m_isRoomCheck)
        {
            m_isRoomCheck = false;
            RoomSetting(room);
        }
        //if (m_testPosition.Contains(m_testRoom.position))
        //{
        //    Debug.Log($"{room.name},{m_testRoom.position}");
        //    RoomSetting(room);
        //    return;
        //}

        m_testPosition.Add(m_testRoom.position);
        room.transform.position = m_testRoom.position;
    }

    Vector3 criteriaVector;
    float criteriafloat;
    bool isXY;
    private void BossRoomSetting(GameObject bossroom)
    {
        float mapSizeX = 18f;
        float mapSizeY = 10f;
        foreach(Vector3 vector in m_testPosition)
        {
            float bossPoX = Mathf.Abs(m_testPosition[0].x + vector.x) / mapSizeX;
            float bossPoY = Mathf.Abs(m_testPosition[0].y + vector.y) / mapSizeY;
            float Po = bossPoX > bossPoY ? bossPoX : bossPoY;

            if (criteriafloat > Po)
            {
                continue;
            }
            
            criteriafloat = Po;
            criteriaVector = vector;
            if(bossPoX > bossPoY)
            {
                isXY = true;
            }
        }

        criteriaVector += isXY switch
        {
            true => criteriaVector.x > 0 ? new Vector3(mapSizeX, 0, 0) : new Vector3(-mapSizeX, 0, 0),
            false => criteriaVector.y > 0 ? new Vector3(0, mapSizeY, 0) : new Vector3(0, mapSizeY, 0)
        };

        bossroom.transform.position = criteriaVector;

    }
}
