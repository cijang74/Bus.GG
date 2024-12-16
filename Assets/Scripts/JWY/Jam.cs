using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : MonoBehaviour
{
    [SerializeField] private float speedReductionFactor = 0.5f; // �̵� �ӵ� ���� ���� (50% ����)
    private float originalSpeed; // ���� �ӵ� ����
    private Rigidbody2D rb; // Rigidbody ����
    private bool isOnGround = false; // ���� ��Ҵ��� Ȯ�ο� �÷���

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            Debug.Log("Jam�� ���� ��ҽ��ϴ�!");

            // Rigidbody �ӵ� �ʱ�ȭ �� Ʈ���� ����
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // �ӵ� ����
                rb.isKinematic = true; // ������ ��ȣ�ۿ� ����
            }

            // �ڽ� ������Ʈ�� CircleCollider2D�� ã�� Ʈ���ŷ� ����
            CircleCollider2D childCollider = GetComponentInChildren<CircleCollider2D>();
            if (childCollider != null)
            {
                childCollider.isTrigger = true; // Ʈ���ŷ� ����
                Debug.Log("�ڽ� �ݶ��̴��� Ʈ���ŷ� ����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("�ڽ� CircleCollider2D�� ã�� �� �����ϴ�.");
            }

            isOnGround = true; // �̹� ó���Ǿ����� �÷��׷� ����
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ Jam�� ���Խ��ϴ�!");

            // Player�� �̵� �ӵ� ����
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                originalSpeed = player.runSpeed; // ���� �ӵ� ����
                player.runSpeed *= speedReductionFactor; // �ӵ� ����
                Debug.Log($"�̵� �ӵ� ����: {player.runSpeed}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ Jam���� ������ϴ�!");

            // Player�� �̵� �ӵ� ����
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.runSpeed = originalSpeed; // ���� �ӵ��� ����
                Debug.Log($"�̵� �ӵ� ����: {player.runSpeed}");
            }
        }
    }
}
