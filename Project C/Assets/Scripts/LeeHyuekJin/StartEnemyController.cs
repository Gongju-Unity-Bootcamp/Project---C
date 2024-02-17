using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 0.1f);
    }

    void StartGame()
    {
        Destroy(gameObject);
    }
}
