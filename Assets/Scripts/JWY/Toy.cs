using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toy : MonoBehaviour
{
    private bool isFading = false;
    private bool isStopped = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private float fadeDuration = 2f; // 사라지는 데 걸리는 시간
    [SerializeField] private GameObject jamPrefab;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 Toy 오브젝트에 없습니다!");
        }
    }

    void FixedUpdate()
    {
        // 멈춘 상태가 아니라면 rb.velocity를 유지
        if (!isStopped && rb != null)
        {
            rb.velocity = Vector2.down * 3f; // 계속해서 떨어지는 속도
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isFading)
        {
            Debug.Log("Toy가 Ground에 닿았습니다!");

            // Rigidbody 동작 멈추기
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 속도 초기화
                rb.isKinematic = true;     // 물리적 상호작용 비활성화
            }

            isStopped = true; // 멈춘 상태로 설정

            // 서서히 사라지기 시작
            StartCoroutine(FadeOutAndDestroy());
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시
            Debug.Log("사망!");
            SceneManager.LoadScene("Ending");
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        isFading = true;

        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // 알파 값 줄이기
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        if (jamPrefab != null)
        {
            Instantiate(jamPrefab, transform.position, Quaternion.identity);
            Debug.Log("jam 프리팹이 생성되었습니다.");
        }
        else
        {
            Debug.LogError("jamPrefab이 할당되지 않았습니다!");
        }

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}
