using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : MonoBehaviour
{
    [SerializeField] GameObject snackPrefab;
    [SerializeField] int weight;
    [SerializeField] float timeToEat;
    private Rigidbody2D rb;
    private bool onGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") && !onGround)
        {
            // 바닥에 닿았을 때
            rb.velocity = Vector2.zero;        // 속도 초기화
            rb.gravityScale = 0f;              // 중력 비활성화
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            onGround = true;
            Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
            Collider2D snackCollider = this.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(snackCollider, playerCollider, false);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 아래로 위치를 조정하여 충돌을 회피
            transform.position += new Vector3(0, -0.1f, 0); // 살짝 아래로 이동
            Debug.Log("스낵이 플레이어와 충돌 -> 위치 조정.");

            // 충돌 무시 설정 (계속 떨어지도록)
            Collider2D playerCollider = other.collider;
            Collider2D snackCollider = this.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(snackCollider, playerCollider, true);

        }
    }


    public int GetWeight()
    {
        return weight;
    }

    public float GetTimeToEat()
    {
        return timeToEat;
    }

    public void Consume()
    {
        // 스낵이 입에 저장될 때
        Debug.Log("Snack stored!");
        onGround = false;
        GameObject snackController = GameObject.Find("SnackController");
        if (snackController != null)
        {
            snackController.GetComponent<SnackController>().CatchedSnack(this.gameObject);
        }
        this.GetComponent<CircleCollider2D>().isTrigger = false; // 트리거 비활성화
        this.gameObject.SetActive(false); // 오브젝트 비활성화
    }

    public void SpitOut(Vector3 position)
    {
        // 스낵을 뱉을 때
        Debug.Log("Snack spit out!");
        onGround = false;
        transform.position = position; // 위치 변경
        this.gameObject.SetActive(true); // 오브젝트 활성화
        this.GetComponent<CircleCollider2D>().isTrigger = false; // 트리거 비활성화
        rb.gravityScale = 1f; // 중력 활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
