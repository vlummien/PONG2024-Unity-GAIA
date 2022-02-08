using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 0.3f;
    void Start()
    {

    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * playerSpeed;

    }
}
