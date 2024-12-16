using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float full = 100f;     //포만감, 계속해서 감소
    [SerializeField] private float decreaseInterval = 1f;   //포만감 감소 간격(초)

    bool eatingSnack = false;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    private float timeSineLastDecrease = 0f;

    private Snack snack;
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
                StartCoroutine(EatSnack());
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
        Vector2 playerVelocity;

        if(eatingSnack)
        {
            playerVelocity = new Vector2(moveInput.x * 0, myRigidbody.velocity.y);
        }
        else
        {
            playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        }

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            snack = other.gameObject.GetComponent<Snack>();
            if (snack != null && nearbySnack == null) // 중복 저장 방지
            {
                nearbySnack = snack;
                Debug.Log("스낵 근처: " + snack.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            nearbySnack = null; // 닿아있는 스낵 초기화
            eatingSnack = false;
            Debug.Log("스낵 멀어짐");
        }
    }

    private void StoreSnack()
    {
        if (storedSnack == null && nearbySnack != null)
        {
            storedSnack = nearbySnack;
            storedSnack.Consume();
            Debug.Log("스낵 저장: " + storedSnack.name);
        }
        else if (storedSnack != null)
        {
            Debug.Log("이미 저장된 스낵이 있습니다!");
        }
    }

    private IEnumerator EatSnack()
    {
        if(nearbySnack != null && storedSnack == null)
        {
            eatingSnack = true;
            yield return new WaitForSeconds(nearbySnack.GetTimeToEat());
            if(nearbySnack == snack)
            {
                full += nearbySnack.GetWeight();    // 포만감 증가
                Debug.Log("스낵 먹음. 포만감: " + full);

                GameObject snackController = GameObject.Find("SnackController");
                if (snackController != null)
                {
                    snackController.GetComponent<SnackController>().CatchedSnack(nearbySnack.gameObject);
                }

                Destroy(nearbySnack.gameObject);
                nearbySnack = null;
                eatingSnack = false;
            }
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
