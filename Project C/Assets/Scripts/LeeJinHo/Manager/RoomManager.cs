using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ����
public enum RoomState
{
    None,
    NotClear,
    Clear
}

//�� ���(����� �±�)�� ���� �� ���� + ���� ����.
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
    
    //�� ���� �����û�� ���� ó��
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
        //�� �渶�� �ֳʹ� �������� üũ
        m_EnemyCount = transform.Find("EnemySpawner").childCount;
        Init();   
    }

    private void Init()
    {
        //�̺�Ʈ�� �޼ҵ� ���
        RoomAppearance_See += RoomAppearanceChanged;

        //�� �±׿� ���� ������ ����
        m_roomRating = gameObject.tag switch
        {
            "NormalRoom" => RoomRating.Normal,
            "KeyRoom" => RoomRating.Key,
            "BossRoom" => RoomRating.Boss,
            _ => throw new ArgumentOutOfRangeException(nameof(gameObject.name)),
        };

    }

    //���� ��Ȱ��ȭ �Ǿ��µ� �÷��̾ �濡 �����ϸ� ���� ���¸� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_roomState == RoomState.None)
        {
            m_roomState = RoomState.NotClear;
        }
    }

    //�� ���¸� üũ�ϰ� �ִٰ� Ŭ��� �ϸ� ���º���
    private void Update()
    {
        if (m_EnemyCount == 0 && m_roomState == RoomState.NotClear)
        {
            m_roomState = RoomState.Clear;
        }
    }
    
    //�� ���°� ����Ǿ� �̺�Ʈ�� �߻��ϸ� ������ �޼ҵ�
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

    // Ŭ����� �� ��޿� ���� ����� �ڽ� Ÿ���� �����ְ� ������û.
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
        //�� ������ ����
    }

    private void CloseDoor()
    {
        //�� ������ ����
    }

}
