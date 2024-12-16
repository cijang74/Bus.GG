using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // TextMeshProUGUI 참조
    private int score = 0;
    private float timer = 0f;

    void Start()
    {
        // 초기 스코어 설정
        UpdateScoreUI();
    }

    void Update()
    {
        // 1초마다 스코어 증가
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            score += 1; // 스코어 증가
            UpdateScoreUI(); // UI 업데이트
            timer = 0f; // 타이머 초기화
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // 스코어 텍스트 업데이트
        }
        else
        {
            Debug.LogWarning("Score TextMeshProUGUI가 설정되지 않았습니다!");
        }
    }
}
