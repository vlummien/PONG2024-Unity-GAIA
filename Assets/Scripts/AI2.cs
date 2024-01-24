using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2 : MonoBehaviour
{
    public float speed = 3.2f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;
    public float rotationSpeed = 1000f;
    
    private Quaternion initialRotation;
    
    private float distanceToBall; // Store the distance between the player and the ball.

    void Start()
    {
        // Store the initial rotation when the script starts
        initialRotation = transform.rotation;
    }

    public void Update()
    {
        
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
    }

    public void FixedUpdate()
    {
        var moveDistance = speed * Time.deltaTime;
        

        if (distanceToBall > ballFarAwayDistance)
        {
            // Spin to win
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Reset to the initial rotation
            transform.rotation = initialRotation;
            
            // Move down/up
            if (ball.transform.position.y > transform.position.y)
            {
                transform.Translate(Vector2.up * moveDistance);
            }
            else if (ball.transform.position.y < transform.position.y)
            {
                transform.Translate((Vector2.down * moveDistance));
            }
        }
    }
}