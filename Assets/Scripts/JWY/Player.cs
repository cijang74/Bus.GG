using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Singleton<Player>
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float full = 100f;     //포만감, 계속해서 감소
    [SerializeField] private float decreaseInterval = 1f;   //포만감 감소 간격(초)

    bool eatingSnack = false;
    float jamDownSpeed = 0.2f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    GameObject snackController;
    private float timeSineLastDecrease = 0f;

    private Snack snack;
    private Snack nearbySnack = null;   //닿아있는 스낵
    private Snack storedSnack = null;   //저장된 스낵

    private float[] seatLeft;
    private float[] seatRight;
    public bool isLight = true;
    public bool isSafe = true;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        snackController = GameObject.Find("SnackController");
        InitSafetyZone();
    }

    void Update()
    {
        Run();
        CheckIsInSafetyZone();
        DecreaseFullness();
        CheckDeadPlayer();

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

    private void InitSafetyZone()
    {
        seatLeft = snackController.GetComponent<SnackController>().GetSeatLeft();
        seatRight = snackController.GetComponent<SnackController>().GetSeatRight();
    }

    private void CheckIsInSafetyZone()
    {
        for(int i = 0; i < seatLeft.Length; i++)
        {
            if(seatLeft[i] < this.transform.position.x && this.transform.position.x < seatRight[i])
            {
                isSafe = true;
                return;
            }
        }
        isSafe = false;
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
            runSpeed *= ((10 - storedSnack.GetWeight()) / 10.0f);
        }
        else if (storedSnack != null)
        {
            Debug.Log("이미 저장된 스낵이 있습니다!");
        }
    }

    private void EatSnack()
    {
        if(nearbySnack != null && storedSnack == null && !eatingSnack)
        {
            if(nearbySnack.GetWeight() != 6)
            {
                eatingSnack = true;
                StartCoroutine(EatSnackCour());
            }
        }
    }

    private IEnumerator EatSnackCour()
    {   
        Snack Snacked = nearbySnack;
        yield return new WaitForSeconds(Snacked.GetTimeToEat());

        if (snackController != null)
        {
            snackController.GetComponent<SnackController>().CatchedSnack(Snacked.gameObject);
        }

        full = Mathf.Min(full + Snacked.GetWeight(), 100f);    // 포만감 증가, 최대값 100 유지
        Debug.Log("스낵 먹음. 포만감: " + full);

        Destroy(Snacked.gameObject);
        nearbySnack = null;
        eatingSnack = false;
    }

    private void SpitOutSnack()
    {
        if (storedSnack != null && isSafe)
        {
            // 플레이어 위치 기준으로 스낵을 뱉음
            Vector3 spitPosition = transform.position + new Vector3(storedSnack.transform.localScale.x, storedSnack.transform.localScale.y, 0f); // 플레이어 오른쪽에 스낵을 뱉음
            storedSnack.SpitOut(spitPosition);
            
            runSpeed /= ((10 - storedSnack.GetWeight()) / 10.0f);
            storedSnack = null;
        }
    }

    private void CheckDeadPlayer()
    {
        if(isLight && !isSafe)
        {
            Debug.Log("사망!");
            SceneManager.LoadScene("Ending");
        }
    }
}
