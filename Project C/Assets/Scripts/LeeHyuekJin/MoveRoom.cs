using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoom : MonoBehaviour
{
    BoxCollider2D m_doorCol;
    Transform m_cameraPo;

    public Vector3 playerInPosition;
    private GameObject navi;
    private AstarPath _astarPath;
    private List<GridGraph> _gridGraphs;
    void Awake()
    {
        m_cameraPo = GameObject.Find("Main Camera").transform;
        m_doorCol = GetComponent<BoxCollider2D>();

        switch (gameObject.name)
        {
            case "LeftDoor":
                playerInPosition = Vector3.left;
                break;
            case "RightDoor":
                playerInPosition = Vector3.right;
                break;
            case "UpDoor":
                playerInPosition = Vector3.up;
                break;
            case "DownDoor":
                playerInPosition = Vector3.down;
                break;
        }
    }
    private void Start()
    {
        navi = GameObject.FindWithTag("GameController");
        _astarPath = navi.GetComponent<AstarPath>();
        _gridGraphs = new List<GridGraph>();
        for (int i = 0; i < Mathf.Min(2, _astarPath.graphs.Length); i++)
        {
            if (_astarPath.graphs[i] is GridGraph)
            {
                _gridGraphs.Add(_astarPath.graphs[i] as GridGraph);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position += playerInPosition * 3;
            m_cameraPo.position = transform.parent.position + new Vector3(0, 0, -10);

            StartCoroutine(OpenDoor(collision.gameObject));
        }
    }

    IEnumerator OpenDoor(GameObject collision)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("¹æÀÌµ¿");

        if (gameObject.name == "UpDoor")
        {
            foreach (GridGraph gridGraph in _gridGraphs)
            {
                gridGraph.center.y += 10;
                gridGraph.Scan();
            }
        }
        else if (gameObject.name == "DownDoor")
        {
            foreach (GridGraph gridGraph in _gridGraphs)
            {
                gridGraph.center.y -= 10;
                gridGraph.Scan();
            }
        }
        else if (gameObject.name == "RightDoor")
        {
            foreach (GridGraph gridGraph in _gridGraphs)
            {
                gridGraph.center.x += 18;
                gridGraph.Scan();
            }
        }
        else if (gameObject.name == "LeftDoor")
        {
            foreach (GridGraph gridGraph in _gridGraphs)
            {
                gridGraph.center.x -= 18;
                gridGraph.Scan();
            }
        }
    }
}
