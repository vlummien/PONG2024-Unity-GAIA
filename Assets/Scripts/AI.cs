using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public float speed = 10;
    public GameObject ball;

    public void FixedUpdate()
    {

        var moveDistance = speed * Time.deltaTime;
        var ballDistance = Mathf.Abs(transform.position.y - ball.transform.position.y);


        if (moveDistance > ballDistance)
        {
            moveDistance = ballDistance / 2;

        }

        if (ball.transform.position.y > transform.position.y)
        {
            transform.Translate(Vector2.up * moveDistance);
        }
        else if (ball.transform.position.y < transform.position.y)
        {
            transform.Translate((Vector2.down * moveDistance));
        }
    }

}
