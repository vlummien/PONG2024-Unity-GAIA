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
    public GameObject ball;
    public float ballFarAwayDistance = 4.5f;

    public AI_BehaviorChanger aiBehaviorChanger;

    private float distanceToBall;
    private Rigidbody2D rigidbody2D;

    [HideInInspector]
    public bool isSuperDefend = false; // When Player defended successfully one time, he gets in the super defend mode

    [HideInInspector] public bool isComingCloser = false;


    [HideInInspector] public bool spinToWin; // Bool to determine whether the player spins or not.

    private Quaternion initialRotation;
    private Vector3 initialPosition;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spinToWin = false;
        initialRotation = transform.rotation;
        initialPosition = transform.position;
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
        DeactivateComingCloser();
        rigidbody2D.velocity = new Vector2();

    }
    
    public void DeactivateSuperDefend()
    {
        // Reset scale
        transform.localScale = new Vector3(0.2f, 1, 1);
        isSuperDefend = false;
    }
    
    private void DeactivateComingCloser()
    {
        transform.position = initialPosition;
        isComingCloser = false;
    }

    private void ActivateSuperDefend()
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

    public void MoveUp()
    {
        transform.Translate((Vector2.up * speed * Time.deltaTime));
    }

    public void MoveDown()
    {
        transform.Translate((Vector2.down * speed * Time.deltaTime));
    }

    public void ComingCloser()
    {
        transform.Translate((Vector2.left * 1 * Time.deltaTime));
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
            if (aiBehaviorChanger.activeBehavior == 0) isComingCloser = true;
            if (aiBehaviorChanger.activeBehavior == 1) ActivateSuperDefend();
        }
    }
}