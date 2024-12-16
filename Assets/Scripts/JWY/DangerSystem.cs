using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSystem : MonoBehaviour
{
    private Transform target;
    private Player playerScript; // Player 스크립트를 참조할 변수

    [SerializeField] private float dangerZoneDuration = 2f;
    [SerializeField] private float spawnHeight = 1f;
    [SerializeField] private GameObject ToyPrefab;
    [SerializeField] private float minSpawnInterval = 5f;
    [SerializeField] private float maxSpawnInterval = 10f;

    void Start()
    {
        // Player 오브젝트 및 Player 스크립트 참조
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            playerScript = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }

        // 코루틴 시작
        StartCoroutine(WaitForPlayerInitialization());
    }

    private IEnumerator WaitForPlayerInitialization()
    {
        // Player의 isSafe 값이 유효해질 때까지 대기
        while (playerScript != null && playerScript.isSafe == false)
        {
            yield return null; // 다음 프레임까지 대기
        }

        // 초기화 완료 후 코루틴 시작
        StartCoroutine(SpawnToyAtIntervals());
    }

    private void Drop()
    {
        // 플레이어가 안전 영역에 있는지 확인
        if (playerScript != null && !playerScript.isSafe)
        {
            Debug.Log("Ooops! Danger Dropped!");
            StartCoroutine(DrawDangerZoneAtPlayer());
        }
        else
        {
            Debug.Log("플레이어가 안전 영역에 있음: Drop 건너뜀");
        }
    }

    private IEnumerator DrawDangerZoneAtPlayer()
    {
        // LineRenderer를 동적으로 생성
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer line = lineObject.AddComponent<LineRenderer>();
        
        line.sortingLayerName = "Default"; // 또는 사용 중인 Sorting Layer 이름
        line.sortingOrder = 90;

        // LineRenderer 기본 설정
        Material lineMaterial = new Material(Shader.Find("Sprites/Default")); // Material 설정
        line.material = lineMaterial;
        line.material.color = new Color(1f, 0f, 0f, 0.5f); // 빨간색, 불투명도 50% 설정
        line.positionCount = 2; // 시작점과 끝점
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.useWorldSpace = true;



        // 라인 시작점과 끝점 설정
        Vector3 playerPosition = target.position; // 플레이어 위치
        Vector3 startPoint = new Vector3(playerPosition.x, playerPosition.y + spawnHeight, -1);
        Vector3 endPoint = new Vector3(playerPosition.x, playerPosition.y - 10, -1);

        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);

        // dangerZoneDuration 후 LineRenderer 제거
        Destroy(lineObject, dangerZoneDuration);

        // 라인 표시 시간 대기
        yield return new WaitForSeconds(dangerZoneDuration);

        // 라인 제거 후 물건 생성 호출
        DropDangerAtPlayer(playerPosition);
    }

    
    private void DropDangerAtPlayer(Vector3 playerPosition)
    {
        // 생성
        Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y + spawnHeight, 0);
        GameObject Toy = Instantiate(ToyPrefab, spawnPosition, Quaternion.identity);


        Rigidbody2D rb = Toy.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 3f; // 아래로 떨어지는 속도
    }
    private IEnumerator SpawnToyAtIntervals()
    {
        while (true)
        {
            Drop();
            // 다음 생성까지 대기 (3~5초 사이의 랜덤 값)
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }
    }
}
