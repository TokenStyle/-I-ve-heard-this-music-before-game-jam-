using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D controller;

    public PlayerStats playerStats; // Player Statistics

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        bool isJump = Input.GetButton("Jump");

        float speed = playerStats.Speed;

        if (Input.GetButton("Run Boost"))
        {
            speed = playerStats.Speed * playerStats.SpeedBoost;
        }

        controller.Move(speed * directionX * Time.fixedDeltaTime, false, isJump);
    }
}
