using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    event Action<RoomState> RoomAppearance_See;
    public static RoomManager Instance { get; private set; }
    public event Action<int> OnEnemyCountChange;
    public static int enemyCount { get; set; }

    private GameObject[] doors;
    private Collider2D[] doorColliders;

    private int doorCount;

    public GameObject normalBox;
    public GameObject goldenBox;



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
        Instance = this;
        Init();
    }
    private void CountingDoor()
    {
        rend = transform.Find("MiniMap").GetComponent<Renderer>();

        doors = transform.GetComponentsInChildren<Transform>(true)
       .Where(child => child.CompareTag("Door"))
       .Select(child => child.gameObject)
       .ToArray();
        Debug.Log($"���� ���� = {doors.Length}");
        doorColliders = new Collider2D[doors.Length];


        for (int i = 0; i < doors.Length; i++)
        {
            doorColliders[i] = doors[i].GetComponent<Collider2D>();
        }
    }
    private void Init()
    {

        RoomAppearance_See += RoomAppearanceChanged;
        RoomAppearance = RoomState.None;
        Debug.Log("�ż��� ����");


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

    private void DropBox()
    {
        int randomNumber = new System.Random().Next(100);
        Debug.Log($"randomNumber = {randomNumber}");


        if (randomNumber < 7)
        {
            if (goldenBox != null)
            {
                Instantiate(goldenBox, transform.position, Quaternion.identity);
            }
        }
        else if (randomNumber < 91)
        {
            if (normalBox != null)
            {
                Instantiate(goldenBox, transform.position, Quaternion.identity);
            }
        }
    }
    private void OpenDoor()
    {
        Debug.Log("���µ���");
        //�� ������ ����
        for (int i = 0; i < doors.Length; i++)
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



}
