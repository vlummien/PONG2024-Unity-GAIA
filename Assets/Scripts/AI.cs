using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private PlayerManagement playerManagement;
    public GameObject ball;

    private void Awake()
    {
        playerManagement = GetComponent<PlayerManagement>();
    }

    public void Update()
    {
        Debug.Log("Source Code AI active");

        if (!playerManagement.BallNear() && playerManagement.isComingCloser)
        {
            playerManagement.ComingCloser();   
        }
        else if (!playerManagement.BallNear())
        {
            Debug.Log("Do nothing");
        }
        else
        {
            bool ballIsAbove = ball.transform.position.y > transform.position.y;
            bool ballIsBelow = ball.transform.position.y < transform.position.y;
            if (ballIsAbove)
            {
                playerManagement.MoveUp();
            }

            if (ballIsBelow)
            {
                playerManagement.MoveDown();
            }
        }
    }
}