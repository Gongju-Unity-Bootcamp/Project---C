using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_rooms;
    
    private BoxCollider2D[] m_roomCollider;

    private void Awake()
    {
        for ( int index = 0; index < m_rooms.Length; ++index ) 
        { 
            GameObject room = m_rooms[index];
            m_rooms[index] = room;
            m_roomCollider[index] = GetComponent<BoxCollider2D>();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
