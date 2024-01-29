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
    
    private GAIA_Manager manager;
    private PlayerManagement playerManagement;

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