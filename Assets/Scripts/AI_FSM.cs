using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GAIA;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM : MonoBehaviour
{
    public float m_Speed = 12f;
    public GameObject ball;

    private Rigidbody2D m_Rigidbody; // Reference used to move the tank.
    private float distance; // Store the distance between the player and the ball.

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
        // When turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
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
        transform.Translate((Vector2.up * m_Speed * Time.deltaTime));
    }

    private void MoveDown()
    {
        transform.Translate((Vector2.down * m_Speed * Time.deltaTime));
    }

    private void DoNothing()
    {
        Debug.Log("Saving Energy");
    }

    public void ExecuteAction(int actionTag)
    {
        switch (actionTag)
        {
            case (int)Tags.ActionTags.NULL:
                DoNothing();
                break;

            case (int)Tags.ActionTags.MOVE_UP:
                MoveUp();
                break;

            case (int)Tags.ActionTags.MOVE_DOWN:
                MoveDown();
                break;
        }
    }

    public List<int> HandleEvents()
    {
        FSMevents.Clear();

        var BallPosition = ball.transform.position.y;
        // Debug.Log("Ball position :" + BallPosition);
        if (BallPosition > transform.position.y)
        {
            FSMevents.Add((int)Tags.EventTags.BALL_ABOVE_NPC);
        }
        
        if (BallPosition < transform.position.y)
        {
            FSMevents.Add((int)Tags.EventTags.BALL_BELOW_NPC);
        }
        
        // TODO: BALL far away

        if (FSMevents.Count == 0)
        {
            Debug.Log("No Events added");
            FSMevents.Add((int)Tags.EventTags.NULL);
        }

        return FSMevents;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 BallPosition = GameObject.Find("Ball").transform.position;
        distance = Vector3.Distance(transform.position, BallPosition);

        m_Rigidbody.isKinematic = false;

        FSMactions = FSM.Update();
        for (int i = 0; i < FSMactions.Count; i++)
        {
            if (FSMactions[i] != (int)Tags.ActionTags.NULL)
            {
                ExecuteAction(FSMactions[i]);
            }
        }
    }
}