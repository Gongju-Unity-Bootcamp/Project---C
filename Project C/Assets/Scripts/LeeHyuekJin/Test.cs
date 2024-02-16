using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject navi;
    private AstarPath _astarPath;
    private List<GridGraph> _gridGraphs;
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpDoor"))
        {
            foreach (GridGraph gridGraph in _gridGraphs)
            {
                gridGraph.center.y += 10;
                gridGraph.Scan();
            }
        }
    }
}
