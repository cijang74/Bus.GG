using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Mouse : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;
    }

}
