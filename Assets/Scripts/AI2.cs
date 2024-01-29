using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2 : MonoBehaviour
{
    private PlayerManagement playerManagement;

    private void Awake()
    {
        playerManagement = GetComponent<PlayerManagement>();
    }

    public void Update()
    {
        Debug.Log("Source Code AI 2 active");

        if (playerManagement.BallNear() && playerManagement.isSuperDefend)
        {
            playerManagement.SuperDefend();
        }
        else if (playerManagement.BallNear())
        {
            playerManagement.Defend();
        }
        else
        {
            playerManagement.ActivateSpin();
        }
    }
}