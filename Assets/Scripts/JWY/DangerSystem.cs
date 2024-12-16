using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSystem : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float dangerZoneDuration = 2f;
    [SerializeField] private float spawnHeight = 1f;
    [SerializeField] private GameObject ToyPrefab;
    [SerializeField] private float minSpawnInterval = 5f;
    [SerializeField] private float maxSpawnInterval = 10f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnToyAtIntervals());
    }

    private void Drop()
    {
        Debug.Log("Ooops!");

        StartCoroutine(DrawDangerZoneAtPlayer());
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
