using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 0.3f;
    [HideInInspector] private Rigidbody2D rigidbody2D;
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    
    
    private void OnEnable()
    {
        rigidbody2D.isKinematic = false;
    }
    
    private void OnDisable()
    {
        // When turned off, set it to kinematic so it stops moving.
        rigidbody2D.isKinematic = true;
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * playerSpeed;
        
        
        
        rigidbody2D.isKinematic = false;

    }
}
