using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GameManager : MonoBehaviour
{
    private static Player_GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Player_GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
