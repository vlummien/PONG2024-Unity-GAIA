using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 0.3f;
    void Start()
    {

    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical2");

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * playerSpeed;


    }
}
