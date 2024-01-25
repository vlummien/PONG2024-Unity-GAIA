using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 3.3f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;
    
    private float distanceToBall; // Store the distance between the player and the ball.
    
    
    private Quaternion initialRotation;

    void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        transform.rotation = initialRotation;
    }

    public void Update()
    {
        Debug.Log("Source Code AI active");
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
  
        var moveDistance = speed * Time.deltaTime;
        

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