using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviController : MonoBehaviour
{
    private AstarPath _astarPath;
    private List<GridGraph> _gridGraphs;
    // Start is called before the first frame update
    void Start()
    {
        _astarPath = GetComponent<AstarPath>();
        _gridGraphs = new List<GridGraph>();
        for (int i = 0; i < Mathf.Min(2, _astarPath.graphs.Length); i++)
        {
            if (_astarPath.graphs[i] is GridGraph)
            {
                _gridGraphs.Add(_astarPath.graphs[i] as GridGraph);
            }
        }
    }
    public void Scan(int door)
    {
        switch(door)
        {
            case 0: //��ĵ
                foreach (GridGraph gridGraph in _gridGraphs)
                {
                    _astarPath.Scan();
                }
            break;

            case 1: //�������
                foreach(GridGraph gridGraph in _gridGraphs)
                {
                    gridGraph.center.y += 10;
                }
            break;

            case 2: //�Ʒ������
                foreach (GridGraph gridGraph in _gridGraphs)
                {
                    gridGraph.center.y -= 10;
                }
            break;

            case 3: //���������
                foreach (GridGraph gridGraph in _gridGraphs)
                {
                    gridGraph.center.x += 18;
                }
                break;

            case 4://���������
                foreach (GridGraph gridGraph in _gridGraphs)
                {
                    gridGraph.center.x -= 18;
                }
                break;
        }
        
    }
}
