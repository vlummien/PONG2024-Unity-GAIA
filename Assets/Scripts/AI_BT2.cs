#define PANDA

using System;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using GAIA;
using Random = UnityEngine.Random;

public class AI_BT2 : MonoBehaviour
{
    public float m_Speed = 3.3f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;
    public float rotationSpeed = 1000f;

    private float distanceToBall; // Store the distance between the player and the ball.
    private Rigidbody2D m_Rigidbody; // Reference used to move the tank.
    private bool spinToWin; // Bool to determine whether the player spins or not.
    private float degreesPerSecond = 200;        // Magnitude of spin of death rotation.

    public string BTFileName; // Choose the BT to load by txt file name.
    private GAIA_Manager manager; // Instatiates the manager.

    private Quaternion initialRotation;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
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
        initialRotation = transform.rotation;
        spinToWin = false;
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
        Debug.Log("BT AI is active");
#endif
    }

    [Task]
    private void Defend()
    {
        Debug.Log("BT AI: Defend!");
        StopSpin();
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

    [Task]
    private void ActivateSpin()
    {
        Debug.Log("BT AI: Spin to win!");
        spinToWin = true;
    }

    [Task]
    private void StopSpin()
    {
        spinToWin = false;
        transform.rotation = initialRotation;
    }
    
    // CONDITIONS

    [Task]
    bool ballNear()
    {
        Debug.Log(distanceToBall);
        return ballFarAwayDistance > distanceToBall;
    }
    
    // CONDITIONS END

    void LateUpdate()
    {
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
        

        m_Rigidbody.isKinematic = false;

        
        if (spinToWin)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}