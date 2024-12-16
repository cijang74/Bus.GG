using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float full = 100f;     //포만감, 계속해서 감소
    [SerializeField] private float decreaseInterval = 1f;   //포만감 감소 간격(초)


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    private float timeSineLastDecrease = 0f;
    private Snack nearbySnack = null;   //닿아있는 스낵
    private Snack storedSnack = null;   //저장된 스낵

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        DecreaseFullness();

        if (nearbySnack != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                StoreSnack();
            }

            if(Mouse.current.rightButton.wasPressedThisFrame)
            {
                EatSnack();
            }
        }

        if (storedSnack != null && Keyboard.current.spaceKey.wasPressedThisFrame) // 스페이스바로 뱉기
        {
            SpitOutSnack();
        }

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;
    }

    private void DecreaseFullness()
    {
        timeSineLastDecrease += Time.deltaTime;     //프레임 시간 누적

        if (timeSineLastDecrease >= decreaseInterval)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            Snack snack = other.gameObject.GetComponent<Snack>();
            if (snack != null)
            {
                nearbySnack = snack; // 닿아있는 스낵 저장
                Debug.Log("스낵 근처: " + snack.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            nearbySnack = null; // 닿아있는 스낵 초기화
            Debug.Log("스낵 멀어짐");
        }
    }

    private void StoreSnack()
    {
        if (storedSnack == null && nearbySnack != null)
        {
            storedSnack = nearbySnack;
            Debug.Log("스낵 저장: " + storedSnack.name);
        }
        else if (storedSnack != null)
        {
            Debug.Log("이미 저장된 스낵이 있습니다!");
        }
    }

    private void EatSnack()
    {
        if (storedSnack != null)
        {
            full += storedSnack.GetWeight(); // 포만감 증가
            Debug.Log("스낵 먹음. 포만감: " + full);
            storedSnack = null; // 저장된 스낵 비움
        }
        else
        {
            Debug.Log("저장된 스낵이 없습니다!");
        }
    }

    private void SpitOutSnack()
    {
        if (storedSnack != null)
        {
            // 플레이어 위치 기준으로 스낵을 뱉음
            Vector3 spitPosition = transform.position + new Vector3(1f, 0f, 0f); // 플레이어 오른쪽에 스낵을 뱉음
            storedSnack.SpitOut(spitPosition);
            storedSnack = null;
        }
        else
        {
            Debug.Log("저장된 스낵이 없습니다!");
        }
    }

}
