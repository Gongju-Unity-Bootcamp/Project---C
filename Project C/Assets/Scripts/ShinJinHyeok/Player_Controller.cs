using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D playerRbody;

    float axisHor;
    float axisVer;
    float moveSpeed = 5.0f;
    float acceleration = 0.5f;
    float deceleration = 0.1f;

    void Start()
    {
        playerRbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        axisHor = Input.GetAxisRaw("Horizontal");
        axisVer = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(axisHor, axisVer);

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }

        if (axisHor != 0 || axisVer != 0)
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, move * moveSpeed, acceleration);
        }
        else
        {
            playerRbody.velocity = Vector2.Lerp(playerRbody.velocity, Vector2.zero, deceleration);
        }
    }
}