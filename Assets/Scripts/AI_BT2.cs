#define PANDA

using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using GAIA;

public class AI_BT2 : MonoBehaviour
{
    public string BTFileName;
    
    private Rigidbody2D m_Rigidbody; 
    private GAIA_Manager manager; 
    private PlayerManagement playerManagement;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        playerManagement = GetComponent<PlayerManagement>();
    }


    private void OnEnable()
    {
        // When turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;
        manager.changeTickOn(gameObject, BehaviourTree.UpdateOrder.Update);
    }

    private void OnDisable()
    {
        // When turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
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
#if (PANDA)
        // Update the Panda Behaviour Tree for checking new state
        manager.changeBT(gameObject, BTFileName);
        Debug.Log("BT AI 2 is active");
#endif
    }
    
    // Actions

    [Task]
    private void Defend()
    {
        playerManagement.Defend();
    }

    [Task]
    private void SuperDefend()
    {
        playerManagement.SuperDefend();
    }

    [Task]
    private void ActivateSpin()
    {
        playerManagement.ActivateSpin();
    }

    [Task]
    private void StopSpin()
    {
        playerManagement.StopSpin();
    }
    
    // Actions End

    // (BT SEQUENCE) CONDITIONS

    [Task]
    bool ballNear()
    {
        return playerManagement.BallNear();
    }

    [Task]
    bool ballNearAndSuperDefend()
    {
        return ballNear() && playerManagement.isSuperDefend;
    }

    // CONDITIONS END
    
}