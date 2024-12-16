using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : MonoBehaviour
{
    [SerializeField] private float speedReductionFactor = 0.5f; // 이동 속도 감소 비율 (50% 감소)
    private Rigidbody2D rb; // Rigidbody 참조
    private bool isOnGround = false; // 땅에 닿았는지 확인용 플래그
    [SerializeField] private float lifetime = 10f; // 10초 뒤 사라지는 시간 설정

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            Debug.Log("Jam이 땅에 닿았습니다!");

            // Rigidbody 속도 초기화 및 트리거 설정
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 속도 멈춤
                rb.isKinematic = true; // 물리적 상호작용 멈춤
            }

            // 자식 오브젝트의 CircleCollider2D를 찾아 트리거로 변경
            BoxCollider2D childCollider = GetComponentInChildren<BoxCollider2D>();
            if (childCollider != null)
            {
                childCollider.isTrigger = true; // 트리거로 변경
                Debug.Log("자식 콜라이더가 트리거로 변경되었습니다.");
            }
            else
            {
                Debug.LogError("자식 CircleCollider2D를 찾을 수 없습니다.");
            }

            isOnGround = true; // 이미 처리되었음을 플래그로 설정
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 Jam에 들어왔습니다!");

            // Player의 이동 속도 조절
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.runSpeed *= speedReductionFactor; // 속도 감소
                Debug.Log($"이동 속도 감소: {player.runSpeed}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 Jam에서 벗어났습니다!");

            // Player의 이동 속도 복원
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.runSpeed /= speedReductionFactor; // 원래 속도로 복원
                Debug.Log($"이동 속도 복원: {player.runSpeed}");
            }
        }
    }
}
