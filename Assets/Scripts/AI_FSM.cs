using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GAIA;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM : MonoBehaviour
{
    public float m_Speed = 3.3f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;

    private Rigidbody2D m_Rigidbody; // Reference used to move the tank.
    private float distanceToBall; // Store the distance between the player and the ball.

    //IA attributes
    private FSM_Machine FSM;
    private GAIA_Manager manager;
    private List<int> FSMactions; // Variable que contiene las acciones a realizar en cada update.
    private List<int> FSMevents = new List<int>();

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
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

    private void MoveUp()
    {
        transform.Translate((Vector2.up * m_Speed * Time.deltaTime ));
    }

    private void MoveDown()
    {
        transform.Translate((Vector2.down * m_Speed * Time.deltaTime ));
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
                MoveUp();
                break;

            case (int)Tags.ActionTags.MOVE_DOWN:
                MoveDown();
                break;
            default:
                DoNothing();
                break;
        }
    }

    public List<int> HandleEvents()
    {
        FSMevents.Clear();


        Debug.Log($"Distance to Ball :{distanceToBall}");
        Debug.Log($"ballFarAwayDistance :{ballFarAwayDistance}");
        if ( distanceToBall > ballFarAwayDistance)
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
                Debug.Log($"BALL_ABOVE_NPC Event added: {actionId}");
            }

            if (ballPositionY < transform.position.y)
            {
                int actionId = (int)Tags.EventTags.BALL_BELOW_NPC;
                FSMevents.Add(actionId);
                Debug.Log($"BALL_BELOW_NPC Event added: {actionId}");
            }
        }

        if (FSMevents.Count == 0)
        {
            int actionId = (int)Tags.EventTags.NULL;
            FSMevents.Add(actionId);
            Debug.Log($"NULL Event added: {actionId}");
        }

        return FSMevents;
    }
    
    void Update()
    {
        Debug.Log("------- Update start -------- ");
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
        

        m_Rigidbody.isKinematic = false;
        
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
        Debug.Log("------- Update end -------- ");
        Debug.Log("FSM active ");
    }
}