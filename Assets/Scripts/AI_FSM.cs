using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GAIA;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM : MonoBehaviour
{
    private PlayerManagement playerManagement;
    public GameObject ball;

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
        FSM = manager.createMachine(this, (int)FA_Classic.FAType.CLASSIC, "PongAIDeterministic");
        addNoEvent();
    }


    private void DoNothing()
    {
        Debug.Log("Saving Energy");
    }

    public void ExecuteAction(int actionTag)
    {
        Debug.Log($"Execute Action: {actionTag}");
        switch (actionTag)
        {
            case (int)Tags.ActionTags.MOVE_UP:
                playerManagement.MoveUp();
                break;

            case (int)Tags.ActionTags.MOVE_DOWN:
                playerManagement.MoveDown();
                break;

            case (int)Tags.ActionTags.COME_CLOSER:
                playerManagement.ComingCloser();
                break;
            
            default:
                DoNothing();
                break;
        }
    }

    public List<int> HandleEvents()
    {
        FSMevents.Clear();


        if (!playerManagement.BallNear() && playerManagement.isComingCloser)
        {
            int actionId = (int)Tags.EventTags.COMING_CLOSER_AVAILABLE;
            FSMevents.Add(actionId);
        }
        else if (!playerManagement.BallNear())
        {
            int actionId = (int)Tags.EventTags.BALL_FAR_AWAY;
            FSMevents.Add(actionId);
        }
        else
        {
            var ballPositionY = ball.transform.position.y;
            if (ballPositionY > transform.position.y)
            {
                int actionId = (int)Tags.EventTags.BALL_ABOVE_NPC;
                FSMevents.Add(actionId);
            }

            if (ballPositionY < transform.position.y)
            {
                int actionId = (int)Tags.EventTags.BALL_BELOW_NPC;
                FSMevents.Add(actionId);
            }
        }

        if (FSMevents.Count == 0)
        {
            int actionId = (int)Tags.EventTags.NULL;
            FSMevents.Add(actionId);
        }

        return FSMevents;
    }

    void Update()
    {
        Debug.Log("FSM active ");

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