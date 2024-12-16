using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] public float full = 100f;     //������, ����ؼ� ����
    [SerializeField] private float decreaseInterval = 1f;   //������ ���� ����(��)


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    private float timeSineLastDecrease = 0f;
    private Snack nearbySnack = null;   //����ִ� ����
    private Snack storedSnack = null;   //����� ����

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

        if (storedSnack != null && Keyboard.current.spaceKey.wasPressedThisFrame) // �����̽��ٷ� ���
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
        timeSineLastDecrease += Time.deltaTime;     //������ �ð� ����

        if (timeSineLastDecrease >= decreaseInterval)
        {
            if (full > 0)
            {
                full -= 1;
                Debug.Log("������ ����: " + full);
            }
            else
            {
                Debug.Log("�������� 0�Դϴ�.");
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
                nearbySnack = snack; // ����ִ� ���� ����
                Debug.Log("���� ��ó: " + snack.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            nearbySnack = null; // ����ִ� ���� �ʱ�ȭ
            Debug.Log("���� �־���");
        }
    }

    private void StoreSnack()
    {
        if (storedSnack == null && nearbySnack != null)
        {
            storedSnack = nearbySnack;
            Debug.Log("���� ����: " + storedSnack.name);
        }
        else if (storedSnack != null)
        {
            Debug.Log("�̹� ����� ������ �ֽ��ϴ�!");
        }
    }

    private void EatSnack()
    {
        if (storedSnack != null)
        {
            full += storedSnack.GetWeight(); // ������ ����
            Debug.Log("���� ����. ������: " + full);
            storedSnack = null; // ����� ���� ���
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�!");
        }
    }

    private void SpitOutSnack()
    {
        if (storedSnack != null)
        {
            // �÷��̾� ��ġ �������� ������ ����
            Vector3 spitPosition = transform.position + new Vector3(1f, 0f, 0f); // �÷��̾� �����ʿ� ������ ����
            storedSnack.SpitOut(spitPosition);
            storedSnack = null;
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�!");
        }
    }

}
