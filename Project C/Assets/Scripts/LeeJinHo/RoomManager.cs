using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject m_Camera;
    private Color redColor = Color.red;
    private Color whiteColor = Color.white;
    private Renderer rend;

    private RoomState m_roomState;
    private RoomRating m_roomRating;
    
    event Action<RoomState> RoomAppearance_See;
    public static RoomManager Instance { get; private set; }
    public event Action<int> OnEnemyCountChange;
    public static int enemyCount { get; set; }

    private GameObject[] doors;
    private Collider2D[] doorColliders;

    public bool isBossRoom { get; set; }

    public Sprite bossRoomDoor;

    //�� ���� �����û�� ���� ó��
    public RoomState RoomAppearance
    { 
        get => m_roomState; 
        set
        {
            Debug.Log("RoomState");

            m_roomState = value;
            RoomAppearance_See?.Invoke(RoomAppearance);
            Debug.Log("�� ���� ����");
        }
    }

    private void Start()
    {
        isBossRoom = false;
        Init();
        Invoke("CheckBossRoom", 0.1f);
    }
    private void CountingDoor()
    {
        rend = transform.Find("MiniMap").GetComponent<Renderer>();

        doors = transform.GetComponentsInChildren<Transform>()
                   .Where(child => child.CompareTag("Door"))
                   .Select(child => child.gameObject)
                   .ToArray();
        doorColliders = new Collider2D[doors.Length];


        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i] = doors[i].GetComponent<Collider2D>();
        }
        Debug.Log($"Ȱ��ȭ�� �����ݶ��̴�{doorColliders.Length}");
    }
    private void Init()
    {
        //�̺�Ʈ�� �޼ҵ� ���
        RoomAppearance_See += RoomAppearanceChanged;
        RoomAppearance = RoomState.None;
        Debug.Log("�ż��� ����");
        //�� �±׿� ���� ������ ����
        /* m_roomRating = gameObject.tag switch
         {
             "NormalRoom" => RoomRating.Normal,
             "KeyRoom" => RoomRating.Key,
             "BossRoom" => RoomRating.Boss,
             _ => throw new ArgumentOutOfRangeException(nameof(gameObject.name)),
         };*/
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        UpdateEnemyCount();

        CountingDoor();
    }

    //���� ��Ȱ��ȭ �Ǿ��µ� �÷��̾ �濡 �����ϸ� ���� ���¸� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_roomState == RoomState.None)
        {
            Debug.Log("Ŭ���� X");
            m_Camera.transform.position = transform.position + new Vector3(0, 0, -10);
            if (rend != null)
            {
                rend.material.color = redColor;
            }
            RoomAppearance = RoomState.NotClear;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && rend != null)
        {
            rend.material.color = whiteColor;
        }
    }

    //�� ���¸� üũ�ϰ� �ִٰ� Ŭ��� �ϸ� ���º���
    private void Update()
    {
        if (enemyCount == 0 && m_roomState == RoomState.NotClear)
        {
            Debug.Log("Ŭ���� O");
            RoomAppearance = RoomState.Clear;
        }
    }
    
    //�� ���°� ����Ǿ� �̺�Ʈ�� �߻��ϸ� ������ �޼ҵ�
    private void RoomAppearanceChanged(RoomState state)
    {
        Debug.Log("����� ����");
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
        /*ItemType type = m_roomRating switch
        {
            RoomRating.Normal => (ItemType)4,
            RoomRating.Key    => (ItemType)5,
            RoomRating.Boss   => (ItemType)5
        };
        Manager.Spawn.SpawnBox(type);*/
    }
    private void OpenDoor()
    {
        Debug.Log("���µ���");
        //�� ������ ����
        for (int i = 0; i < doors.Length - 2; i++)
        {
            Debug.Log("������");
            doorColliders[i].isTrigger = false;
        }
    }

    private void CloseDoor()
    {
        Debug.Log("Ŭ�ν�����");
        //�� ������ ����
        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i].isTrigger = true;
        }
    }

    public void UpdateEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        OnEnemyCountChange?.Invoke(enemyCount);
        Debug.Log($"���� �� �� : {enemyCount}");
    }

    private void CheckBossRoom()
    {
        if(!isBossRoom && doors.Length ==1)
        {
            isBossRoom = true;
            Debug.Log($"{gameObject.name}�� �������Դϴ�.");
        }

        if(isBossRoom)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Door"))
                {
                    SpriteRenderer doorSprite = collider.GetComponent<SpriteRenderer>();
                    if (doorSprite != null)
                    {
                        doorSprite.sprite = bossRoomDoor;
                    }
                }
            }
        }
    }
    
}
