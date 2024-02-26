using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject blood;
    public GameObject[] heart;
    public static event Action OnEnemySpawned;
    public static event Action OnEnemyDestroyed;
    private void Start()
    {
        OnEnemySpawned?.Invoke();
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            int randomNum = UnityEngine.Random.Range(0, 100);
            if (blood != null)
            {
                Instantiate(blood, transform.position, Quaternion.identity);
            }
            if(heart.Length > 0 && randomNum < 3)
            {
                Instantiate(heart[0], transform.position, Quaternion.identity);
            }
            else if(heart.Length > 0 && randomNum < 10)
            {
                Instantiate(heart[1], transform.position, Quaternion.identity);
            }
            OnEnemyDestroyed?.Invoke();
        }
    }

}
