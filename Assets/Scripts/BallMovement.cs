using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    float BallSpeed = 5f;
    public float BallDirectionX;
    public float BallDirectionY;
    bool Repeat = false;


    public void Start()
    {

        if (Input.GetKey(KeyCode.Return))
        {

            while (Repeat) { }

            BallDirectionX = Random.Range(1f, 0.5f);
            BallDirectionY = Random.Range(-1f, -0.5f);

            if (BallDirectionY != 0 && BallDirectionX != 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(BallDirectionX, BallDirectionY) * BallSpeed;
                Repeat = false;
            }

            else
            {
                Repeat = true;
            }
        }
    }

    public void Update()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -1, 1);

            if (Input.GetKey(KeyCode.Return))
            {
                Start();
            }
        }
    }
}
