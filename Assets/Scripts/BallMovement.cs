using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{

    float BallSpeed = 5f;
    public float BallDirectionX;
    public float BallDirectionY;
    bool Repeat = false;

    [HideInInspector] public bool isMovingToLeftSide = false;
    private Rigidbody2D rigidbody2D;


    public void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.Return))
        {

            while (Repeat) { }

            BallDirectionX = Random.Range(1f, 0.5f);
            BallDirectionY = Random.Range(-1f, -0.5f);

            if (BallDirectionY != 0 && BallDirectionX != 0)
            {
                rigidbody2D.velocity = new Vector2(BallDirectionX, BallDirectionY) * BallSpeed;
                Repeat = false;
            }

            else
            {
                Repeat = true;
            }
        }
    }

    public void Update()
    {
        ResetBall();
        isMovingToLeftSide = rigidbody2D.velocity.x < 0;
    }

    public void ResetBall()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.transform.position = new Vector3(0, -1, 1);

            if (Input.GetKey(KeyCode.Return))
            {
                Start();
            }
        }
    }
}
