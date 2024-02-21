using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 룸 상태
public enum RoomState
{
    None,
    NotClear,
    Clear
}

//룸 등급(현재는 태그)에 따라 룸 사운드 + 영상 여부.
public enum RoomRating
{
    None,
    Normal,
    Key,
    Boss
}
public class RoomManager : MonoBehaviour
{
    private int m_EnemyCount;
    private RoomState m_roomState;
    private RoomRating m_roomRating;
    
    event Action<RoomState> RoomAppearance_See;
    
    //룸 상태 변경요청이 오면 처리
    public RoomState RoomAppearance
    { 
        get => m_roomState; 
        set
        {
            if ((int)m_roomState >= 2)
            {
                return;                
            }
            m_roomState = value;
            RoomAppearance_See?.Invoke(RoomAppearance);
        }
    }

    private void Start()
    {
        //각 방마다 애너미 마리수를 체크
        m_EnemyCount = transform.Find("EnemySpawner").childCount;
        Init();   
    }

    private void Init()
    {
        //이벤트에 메소드 등록
        RoomAppearance_See += RoomAppearanceChanged;

        //룸 태그에 따라 방등급을 변경
        m_roomRating = gameObject.tag switch
        {
            "NormalRoom" => RoomRating.Normal,
            "KeyRoom" => RoomRating.Key,
            "BossRoom" => RoomRating.Boss,
            _ => throw new ArgumentOutOfRangeException(nameof(gameObject.name)),
        };

    }

    //방이 비활성화 되었는데 플레이어가 방에 입장하면 방의 상태를 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_roomState == RoomState.None)
        {
            m_roomState = RoomState.NotClear;
        }
    }

    //방 상태를 체크하고 있다가 클리어를 하면 상태변경
    private void Update()
    {
        if (m_EnemyCount == 0 && m_roomState == RoomState.NotClear)
        {
            m_roomState = RoomState.Clear;
        }
    }
    
    //룸 상태가 변경되어 이벤트가 발생하면 실행할 메소드
    private void RoomAppearanceChanged(RoomState state)
    {
        switch (state)
        {
            case RoomState.None:
                break;
            case RoomState.NotClear:
                CloseDoor();
                break;
            case RoomState.Clear:
                OpenDoor();
                DropBox();
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(state));

        }

    }

    // 클리어시 방 등급에 따라 드롭할 박스 타입을 정해주고 스폰요청.
    private void DropBox()
    {
        ItemType type = m_roomRating switch
        {
            RoomRating.Normal => (ItemType)4,
            RoomRating.Key    => (ItemType)5,
            RoomRating.Boss   => (ItemType)5
        };
        Manager.Spawn.SpawnBox(type);
    }
    private void OpenDoor()
    {
        //문 열리는 내용
    }

    private void CloseDoor()
    {
        //문 닫히는 내용
    }

}
