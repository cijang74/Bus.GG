using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : MonoBehaviour
{
    [SerializeField] GameObject snackPrefab;
    [SerializeField] int weight;
    private Rigidbody2D rb;
    private bool onGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            // 스낵이 이미 땅에 닿았을 경우 추가 동작 방지
            if (!onGround)
            {
                rb.velocity = Vector2.zero;        // 속도 초기화
                rb.gravityScale = 0f;              // 중력 비활성화
                this.GetComponent<CircleCollider2D>().isTrigger = true;
                onGround = true; // 땅에 닿았음을 표시
                Debug.Log("스낵이 땅에 닿음. onGround: " + onGround);
            }
        }
    }


    public int GetWeight()
    {
        return weight;
    }

    public void Consume()
    {
        // 스낵이 입에 저장될 때
        Debug.Log("Snack stored!");
        onGround = false;
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
