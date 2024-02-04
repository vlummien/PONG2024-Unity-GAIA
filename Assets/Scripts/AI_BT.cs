#define PANDA

using System;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using GAIA;

public class AI_BT : MonoBehaviour
{
    private PlayerManagement playerManagement;
    public GameObject ball;
    
    public string BTFileName; // Choose the BT to load by txt file name.
    private GAIA_Manager manager; // Instatiates the manager.
    


    private void Awake()
    {
        playerManagement = GetComponent<PlayerManagement>();
    }


    private void OnEnable()
    {
        manager?.changeTickOn(gameObject, BehaviourTree.UpdateOrder.Update);
    }

    private void OnDisable()
    {
        manager.changeTickOn(gameObject, BehaviourTree.UpdateOrder.Manual);
    }


    void Start()
    {
        manager = GAIA_Controller.INSTANCE.m_manager;
#if (PANDA)
        manager.createBT(gameObject, BTFileName);
#endif
    }

    void Update()
    {
        Debug.Log("BT AI is active");
#if (PANDA)
        // Update the Panda Behaviour Tree for checking new state
        // Otherwise it is not checking the current state anymore
        manager.changeBT(gameObject, BTFileName);
#endif
    }

    [Task]
    private void MoveUp()
    {
        playerManagement.MoveUp();
    }

    [Task]
    private void MoveDown()
    {
        playerManagement.MoveDown();
    }

    [Task]
    private void ComeCloser()
    {
        playerManagement.ComingCloser();
    }

    [Task]
    private void DoNothing()
    {
        Debug.Log("BT AI: Saving Energy");
    }

    // CONDITIONS

    [Task]
    bool ballFar()
    {
        return !playerManagement.BallNear();
    }

    [Task]
    bool ballAbove()
    {
        return ball.transform.position.y > transform.position.y;
    }

    [Task]
    bool ballBelow()
    {
        Debug.Log(ball.transform.position.y);
        return ball.transform.position.y < transform.position.y;
    }

    [Task]
    bool comeCloserAvailable()
    {
        return !playerManagement.BallNear() && playerManagement.isComingCloser;
    }
    
}