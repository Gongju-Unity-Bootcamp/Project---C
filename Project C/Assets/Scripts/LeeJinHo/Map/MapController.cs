using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomPosition
{
    Left,
    Right,
    Up,
    Down
}
public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_rooms;
    //private BoxCollider2D[] m_roomCollider;
    private RandomPosition m_randomPosition;

    private const int ROOMCOUNT = 10;
    private List<Vector3> m_testPosition;

    private void Awake()
    {
        m_testPosition = new List<Vector3>();
        m_rooms = new GameObject[ROOMCOUNT];
        for ( int index = 0; index < m_rooms.Length; ++index ) 
        { 
            GameObject m_room = transform.Find($"TileMap ({index})").gameObject;
            m_rooms[index] = m_room;
            //m_roomCollider[index] = GetComponent<BoxCollider2D>();
            RoomSetting(m_rooms[index]);
        }
    }

    void Start()
    {

    }

    void RoomSetting(GameObject room)
    {
        Vector3 rePosition = Vector3.zero;
        Vector3 m_localPosition = transform.localPosition;

        m_randomPosition = (RandomPosition)Random.Range(0, 4);
        switch (m_randomPosition)
        {
            case RandomPosition.Left:
                rePosition = new Vector3(transform.localPosition.x - 18, 0, transform.localPosition.z);
                break;
            case RandomPosition.Right:
                rePosition = new Vector3(transform.localPosition.x + 18, 0, transform.localPosition.z);
                break;
            case RandomPosition.Up:
                rePosition = new Vector3(transform.localPosition.x, transform.localPosition.z + 10, 0);
                break;
            case RandomPosition.Down:
                rePosition = new Vector3(transform.localPosition.x, transform.localPosition.z - 10, 0);
                break;
        }

        transform.position = rePosition;

        if (m_testPosition.Contains(transform.position))
        {
            RoomSetting(room);
            return;
        }
        room.transform.position = transform.position;
        m_testPosition.Add(transform.position);
    }

}
