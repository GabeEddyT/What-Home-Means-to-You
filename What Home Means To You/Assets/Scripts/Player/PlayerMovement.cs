﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float midairMovespeed;
    public float brakeSpeed;
    public float manualBrakeSpeed;

    [Header("Jump")]
    public float jumpSpeed;
    public float lowJumpMultiplier;
    public float fallSpeed;
    public float fastFallSpeed;

    [Header("Groundcheck")]
    public float groundRaycastDistance;
    public LayerMask groundCheckLayers;

    ControlButtons buttonSuite;
    SpriteRenderer renderer;

    bool grounded;

    public bool fastFall = false;
    //float ;
    Rigidbody2D physics;

    private void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        buttonSuite = GetComponent<ControlButtons>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        grounded = checkIfGrounded();

        handleMovement();
        handleJump();

        if (grounded)
            fastFall = false;

        debugRaycasts();
    }

    void handleMovement()
    {
        Vector2 currentVelocity = physics.velocity;

        int direction = 0;

        if(Input.GetKey(buttonSuite.left))
        {
            direction = -1;
        }
        else if(Input.GetKey(buttonSuite.right))
        {
            direction = 1;
        }

        if (direction == 0) //if there was no direction of movement brake normally
        {
            currentVelocity += -physics.velocity.normalized * brakeSpeed * Time.deltaTime;
        }
        else
        {
            float speed = grounded ? moveSpeed : midairMovespeed;

            Vector2 newVel = Vector2.right * direction * speed * Time.deltaTime;

            if (newVel.x < 0 && physics.velocity.x > 0 || newVel.x > 0 && physics.velocity.x < 0) //if direction of movement is opposite current velocity brake manually
            {
                currentVelocity += Vector2.right * direction * manualBrakeSpeed * Time.deltaTime;
            }
            else
            {
                currentVelocity = newVel;
            }
        }

        currentVelocity.y = physics.velocity.y;

        physics.velocity = currentVelocity;
    }

    void handleJump()
    {
        if(grounded && Input.GetKeyDown(buttonSuite.up))
        {
            physics.velocity = Vector2.up * jumpSpeed;
        }

        if (!grounded && !fastFall && Input.GetKeyDown(buttonSuite.down))
        {
            fastFall = true;
            physics.velocity = new Vector2(physics.velocity.x, 0);
        }

        if(physics.velocity.y > 0 && !Input.GetKey(buttonSuite.up)) //if player is no longer holding jump button
        {
            physics.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if(physics.velocity.y < 0 && fastFall) //if player has pressed fastfall
        {
            physics.velocity += Vector2.up * Physics2D.gravity.y * (fastFallSpeed - 1) * Time.deltaTime;
        }
        else
        {
            physics.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }
    }


    bool checkIfGrounded()
    {
        bool result = Physics2D.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundCheckLayers.value);

        Debug.Log(result);

        return result;
    }

    void debugRaycasts()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundRaycastDistance, grounded ? Color.red : Color.green);
    }
}