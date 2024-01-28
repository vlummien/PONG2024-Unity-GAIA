using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public float speed = 3.3f;
    public float rotationSpeed = 1000f;
    public float superDefendScaleFactor = 1.5f;
    public float superDefendSpeedFactor = 1.1f;
    public GameObject ball;public float ballFarAwayDistance = 4.5f;

    private float distanceToBall;

    [HideInInspector]
    public bool isSuperDefend = false; // When Player defended successfully one time, he gets in the super defend mode
    [HideInInspector] public bool spinToWin; // Bool to determine whether the player spins or not.

    private Quaternion initialRotation;


    private void Start()
    {
        spinToWin = false;
        initialRotation = transform.rotation;
    }
    
    void LateUpdate()
    {
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
        
        
        if (spinToWin)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void OnGamePointEnd()
    {
        DeactivateSuperDefend();
    }

    // When SuperDefend is activated -> get bigger
    public void DeactivateSuperDefend()
    {
        // Reset scale
        transform.localScale = new Vector3(0.2f, 1, 1);
        isSuperDefend = false;
    }
    public void ActivateSuperDefend()
    {
        if (!isSuperDefend)
        {
            Vector3 biggerScale = new Vector3(0.2f, transform.localScale.y * superDefendScaleFactor, 1);
            transform.localScale = biggerScale;
        }

        isSuperDefend = true;
    }
    
    // Actions 

    public void SuperDefend()
    {
        StopSpin();
        var ballPositionY = ball.transform.position.y;
        float newSpeed = speed * Time.deltaTime * superDefendSpeedFactor;
        Vector2 direction = ballPositionY > transform.position.y ? Vector2.up : Vector2.down;

        transform.Translate((direction * newSpeed));
    }
    
    public void ActivateSpin()
    {
        spinToWin = true;
    }
    
    public void StopSpin()
    {
        spinToWin = false;
        transform.rotation = initialRotation;
    }
    
    public void Defend()
    {
        StopSpin();
        
        var ballPositionY = ball.transform.position.y;
        Vector2 direction = ballPositionY > transform.position.y ? Vector2.up : Vector2.down;
        transform.Translate((direction * speed * Time.deltaTime));
    }
    
    // Actions End
    
    // States
    
    public bool BallNear()
    {
        return ballFarAwayDistance > distanceToBall;
    }
    
    // States End

    private void OnCollisionExit2D(Collision2D other)
    {
        // When successfully defended the ball (ball flies also back to the other player) activate superdefend
        if (other.rigidbody != null && other.rigidbody.velocity.x < 0)
        {
            ActivateSuperDefend();
        }
    }
}