using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GAIA;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM2 : MonoBehaviour
{
    private PlayerManagement playerManagement;

    //IA attributes
    private FSM_Machine FSM;
    private GAIA_Manager manager;
    private List<int> FSMactions; // Variable que contiene las acciones a realizar en cada update.
    private List<int> FSMevents = new List<int>();


    private void Awake()
    {
        playerManagement = GetComponent<PlayerManagement>();
    }

    private void OnDisable()
    {
        manager.deleteFSM(FSM.getFSM());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void addNoEvent()
    {
        FSMevents.Add((int)Tags.EventTags.NULL);
    }

    void Start()
    {
        manager = GAIA_Controller.INSTANCE.m_manager;
        FSM = manager.createMachine(this, (int)FA_Classic.FAType.CLASSIC, "PongAIDeterministic2");
        addNoEvent();
    }

    private void DoNothing()
    {
        Debug.Log("Saving Energy");
    }

    public void ExecuteAction(int actionTag)
    {
        switch (actionTag)
        {
            case (int)Tags.ActionTags.DEFEND:
                playerManagement.StopSpin();
                playerManagement.Defend();
                break;

            case (int)Tags.ActionTags.SPIN:
                playerManagement.ActivateSpin();
                break;

            case (int)Tags.ActionTags.SUPERDEFEND:
                playerManagement.SuperDefend();
                break;

            default:
                DoNothing();
                break;
        }
    }

    public List<int> HandleEvents()
    {
        FSMevents.Clear();

        if (playerManagement.BallNear() && playerManagement.isSuperDefend)
        {
            int actionId = (int)Tags.EventTags.SUPERDEFENDING_AVAILABLE;
            FSMevents.Add(actionId);
        }
        else if (playerManagement.BallNear())
        {
            int eventId = (int)Tags.EventTags.BALL_NEAR;
            FSMevents.Add(eventId);
        }
        else
        {
            Debug.Log("Far Way");
            int eventId = (int)Tags.EventTags.BALL_FAR_AWAY;
            FSMevents.Add(eventId);
        }

        if (FSMevents.Count == 0)
        {
            int eventId = (int)Tags.EventTags.NULL;
            FSMevents.Add(eventId);
            Debug.Log($"NULL Event added: {eventId}");
        }

        return FSMevents;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("FSM 2 active ");

        FSMactions = FSM.Update();
        foreach (int action in FSMactions)
        {
            Debug.Log($"Action: {action}");
        }

        for (int i = 0; i < FSMactions.Count; i++)
        {
            // if (FSMactions[i] != (int)Tags.ActionTags.NULL)
            // {
            ExecuteAction(FSMactions[i]);
            // }
        }
    }
}