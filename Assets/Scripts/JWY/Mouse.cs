using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Mouse : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float full = 100f;     //포만감, 계속해서 감소
    [SerializeField] private float decreaseInterval = 1f;   //포만감 감소 간격(초)


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    private float timeSineLastDecrease = 0f;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        DecreaseFullness();
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

    private void DecreaseFullness()
    {
        timeSineLastDecrease += Time.deltaTime;     //프레임 시간 누적

        if(timeSineLastDecrease >= decreaseInterval)
        {
            if (full > 0)
            {
                full -= 1;
                Debug.Log("포만감 감소: " + full);
            }
            else
            {
                Debug.Log("포만감이 0입니다.");
            }

            timeSineLastDecrease = 0f;
        }
    }

}
