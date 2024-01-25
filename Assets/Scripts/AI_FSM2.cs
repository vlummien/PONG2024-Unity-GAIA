using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GAIA;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM2 : MonoBehaviour
{
    public float m_Speed = 3.3f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;
    public float rotationSpeed = 1000f;

    private Rigidbody2D m_Rigidbody; // Reference used to move the tank.
    private float distanceToBall; // Store the distance between the player and the ball.
    private bool spinToWin; // Bool to determine whether the player spins or not.
    private Quaternion initialRotation;

    //IA attributes
    private FSM_Machine FSM;
    private GAIA_Manager manager;
    private List<int> FSMactions; // Variable que contiene las acciones a realizar en cada update.
    private List<int> FSMevents = new List<int>();
    

    private void Awake()
    {
        initialRotation = transform.rotation;
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        transform.rotation = initialRotation;
    }


    private void OnDisable()
    {
        // When turned off, set it to kinematic so it stops moving.
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
        FSM = manager.createMachine(this, (int)FA_Classic.FAType.CLASSIC, "PongAIDeterministic2");
        addNoEvent();
    }

    private void Defend()
    {
        var ballPositionY = ball.transform.position.y;
        if (ballPositionY > transform.position.y)
        {
            transform.Translate((Vector2.up * m_Speed * Time.deltaTime));
        }

        if (ballPositionY < transform.position.y)
        {
            transform.Translate((Vector2.down * m_Speed * Time.deltaTime));
        }
    }

    private void DoNothing()
    {
        Debug.Log("Saving Energy");
    }

    private void SpinToWin()
    {
        spinToWin = true;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void StopSpin()
    {
        spinToWin = false;
        transform.rotation = initialRotation;
    }

    public void ExecuteAction(int actionTag)
    {
        Debug.Log($"Execute Action: {actionTag}");
        switch (actionTag)
        {
            case (int)Tags.ActionTags.DEFEND:
                StopSpin();
                Defend();
                break;

            case (int)Tags.ActionTags.SPIN:
                SpinToWin();
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
        if (distanceToBall > ballFarAwayDistance)
        {
            int eventId = (int)Tags.EventTags.BALL_FAR_AWAY;
            FSMevents.Add(eventId);
        }
        else
        {
            int eventId = (int)Tags.EventTags.BALL_NEAR;
            FSMevents.Add(eventId);
            Debug.Log($"BALL_NEAR Event added: {eventId}");
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