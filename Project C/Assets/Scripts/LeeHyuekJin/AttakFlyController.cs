using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakFlyController : MonoBehaviour
{
    AIPath _aiPath;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
