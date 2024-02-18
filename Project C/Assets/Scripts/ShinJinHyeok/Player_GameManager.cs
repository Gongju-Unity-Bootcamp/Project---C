using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GameManager : MonoBehaviour
{
    public static Player_GameManager instance = null;
    public GameObject playerObject;
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
    public void GameOver()
    {
        Debug.Log("»ç¸Á!");
        Debug.Log("Game Over!");

        Player_Move.gameState = "gameover";

        Rigidbody2D playerRbody = playerObject.GetComponent<Rigidbody2D>();
        playerRbody.velocity = Vector2.zero;
    }
}