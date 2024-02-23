using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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

    event Action<RoomState> RoomAppearance_See;

    public static int enemyCount { get; set; }

    private GameObject[] doors;
    private Collider2D[] doorColliders;

    public bool isBossRoom { get; set; }
    private int doorCount;

    public GameObject normalBox;
    public GameObject goldenBox;

    public GameObject[] spawnEnemy;
    private bool isClear;

    //룸 상태 변경요청이 오면 처리
    public RoomState RoomAppearance
    {
        get => m_roomState;
        set
        {
            m_roomState = value;
            RoomAppearance_See?.Invoke(RoomAppearance);
        }
    }

    private void Start()
    {
        enemyCount = 0;
        Enemy.OnEnemySpawned += HandleEnemySpawned;
        Enemy.OnEnemyDestroyed += HandleEnemyDestroyed;

        isBossRoom = false;
        Init();
        Invoke("CheckBossRoom", 1.1f);
    }

    private void Init()
    {
        RoomAppearance_See += RoomAppearanceChanged;
        RoomAppearance = RoomState.None;
        rend = transform.Find("MiniMap").GetComponent<Renderer>();
    }
    private void HandleEnemySpawned()
    {
        enemyCount++;
    }
    private void HandleEnemyDestroyed()
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
    }
    private void OnDestroy()
    {
        Enemy.OnEnemySpawned -= HandleEnemySpawned;
        Enemy.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }
    //방이 비활성화 되었는데 플레이어가 방에 입장하면 방의 상태를 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_roomState == RoomState.None)
        {
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
        if (enemyCount == 0 && (m_roomState == RoomState.NotClear || m_roomState == RoomState.None))
        {
            RoomAppearance = RoomState.Clear;
            GameObject player = GameObject.FindWithTag("Player");
            PlayerStats _playerStats = player.GetComponent<PlayerStats>();
            _playerStats.ClearRoom();
        }
        if (enemyCount != 0 && (m_roomState == RoomState.None || m_roomState == RoomState.Clear))
        {
            RoomAppearance = RoomState.NotClear;
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

    [Obsolete("드랍확률 확인하세요. Goldenbox: 7, NormalBox: 21")]
    private void DropBox()
    {
        int randomNumber = new System.Random().Next(100);
        Debug.Log($"randomNumber = {randomNumber}");

        if (randomNumber < 50)
        {
            if (goldenBox != null)
            {
                Instantiate(goldenBox, transform.position, Quaternion.identity);
            }
        }
        else if (randomNumber < 100)
        {
            if (normalBox != null)
            {
                Instantiate(normalBox, transform.position, Quaternion.identity);
            }
        }
    }
    private void OpenDoor()
    {
        doors = transform.GetComponentsInChildren<Transform>()
                   .Where(child => child.CompareTag("Door"))
                   .Select(child => child.gameObject)
                   .ToArray();
        doorColliders = new Collider2D[doors.Length];

        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i] = doors[i].GetComponent<Collider2D>();
        }
        //문 열리는 내용
        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i].isTrigger = false;
        }
    }

    private void CloseDoor()
    {
        doors = transform.GetComponentsInChildren<Transform>()
                   .Where(child => child.CompareTag("Door"))
                   .Select(child => child.gameObject)
                   .ToArray();
        doorColliders = new Collider2D[doors.Length];


        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i] = doors[i].GetComponent<Collider2D>();
        }
        //문 닫히는 내용
        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i].isTrigger = true;
        }
    }
}
