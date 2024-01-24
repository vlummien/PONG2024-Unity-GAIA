#define PANDA

using System;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using GAIA;
using Random = UnityEngine.Random;

public class AI_BT : MonoBehaviour
{
    public float m_Speed = 3.3f;
    public float ballFarAwayDistance = 4.5f;
    public GameObject ball;

    private float distanceToBall; // Store the distance between the player and the ball.
    private Rigidbody2D m_Rigidbody; // Reference used to move the tank.
    private bool spinToWin; // Bool to determine whether the player spins or not.
    private float degreesPerSecond = 200;        // Magnitude of spin of death rotation.

    public string BTFileName; // Choose the BT to load by txt file name.
    private GAIA_Manager manager; // Instatiates the manager.

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        // When turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        // When turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
        manager.changeBT(gameObject, BTFileName);
    }
    

    void Start()
    {
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
#endif
    }

    [Task]
    private void MoveUp()
    {
        Debug.Log("BT AI: MoveUp");
        transform.Translate((Vector2.up * m_Speed * Time.deltaTime));
    }

    [Task]
    private void MoveDown()
    {
        Debug.Log("BT AI: MoveDown");
        transform.Translate((Vector2.down * m_Speed * Time.deltaTime));
    }

    [Task]
    private void DoNothing()
    {
        Debug.Log("BT AI: Saving Energy");
    }

    [Task]
    bool ActivateSpin()
    {
        spinToWin = true;
        return true;
    }
    
    // CONDITIONS

    [Task]
    bool ballFar()
    {
        Debug.Log(distanceToBall);
        return distanceToBall > ballFarAwayDistance;
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
    
    // CONDITIONS END

    [Task]
    bool StopSpin()
    {
        spinToWin = false;
        return true;
    }
    
    bool changeColor()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        Color random = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = random;
        }

        return true;
    }

    void LateUpdate()
    {
        Vector3 BallPosition = ball.transform.position;
        distanceToBall = Mathf.Abs(transform.position.x - BallPosition.x);
        

        m_Rigidbody.isKinematic = false;

        
        if (spinToWin)
        {
            transform.Rotate(Vector3.up * degreesPerSecond * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.left * degreesPerSecond * Time.deltaTime, Space.Self);
        }
    }
}