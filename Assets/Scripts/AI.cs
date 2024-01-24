using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 10;
    public float ballFarAwayDistance = 5f;
    public GameObject ball;
    
    private float distanceToBall; // Store the distance between the player and the ball.

    public void Update()
    {
        
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
    }

    public void FixedUpdate()
    {
        var moveDistance = speed * Time.deltaTime;


        // var ballDistance = Mathf.Abs(transform.position.y - ball.transform.position.y);
        // if (moveDistance > ballDistance)
        // {
        //     moveDistance = ballDistance / 2;
        // }

        if (distanceToBall > ballFarAwayDistance)
        {
            Debug.Log("Do nothing");
        }
        else
        {
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