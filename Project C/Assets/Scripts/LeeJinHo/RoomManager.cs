using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //룸 상태 변경요청이 오면 처리
    public RoomState RoomAppearance
    { 
        get => m_roomState; 
        set
        {
            Debug.Log("RoomState");

            m_roomState = value;
            RoomAppearance_See?.Invoke(RoomAppearance);
            Debug.Log("룸 상태 변경");
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
        Debug.Log($"활성화된 도어콜라이더{doorColliders.Length}");
    }
    private void Init()
    {
        //이벤트에 메소드 등록
        RoomAppearance_See += RoomAppearanceChanged;
        RoomAppearance = RoomState.None;
        Debug.Log("매서드 시작");
        //룸 태그에 따라 방등급을 변경
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

    //방이 비활성화 되었는데 플레이어가 방에 입장하면 방의 상태를 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_roomState == RoomState.None)
        {
            Debug.Log("클리어 X");
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

    //방 상태를 체크하고 있다가 클리어를 하면 상태변경
    private void Update()
    {
        if (enemyCount == 0 && m_roomState == RoomState.NotClear)
        {
            Debug.Log("클리어 O");
            RoomAppearance = RoomState.Clear;
        }
    }
    
    //룸 상태가 변경되어 이벤트가 발생하면 실행할 메소드
    private void RoomAppearanceChanged(RoomState state)
    {
        Debug.Log("룸상태 변경");
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
        Debug.Log("오픈도어");
        //문 열리는 내용
        for (int i = 0; i < doors.Length - 2; i++)
        {
            Debug.Log("문열기");
            doorColliders[i].isTrigger = false;
        }
    }

    private void CloseDoor()
    {
        Debug.Log("클로스도어");
        //문 닫히는 내용
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
        Debug.Log($"현재 적 수 : {enemyCount}");
    }

    private void CheckBossRoom()
    {
        if(!isBossRoom && doors.Length ==1)
        {
            isBossRoom = true;
            Debug.Log($"{gameObject.name}은 보스룸입니다.");
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
