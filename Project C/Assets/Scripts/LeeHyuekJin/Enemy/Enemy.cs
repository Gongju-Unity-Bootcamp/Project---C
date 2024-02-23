using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject blood;

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
            if (blood != null)
            {
                Instantiate(blood, transform.position, Quaternion.identity);
            }
            OnEnemyDestroyed?.Invoke();
        }
    }

}
