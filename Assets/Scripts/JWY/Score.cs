using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // TextMeshProUGUI ����
    private int score = 0;
    private float timer = 0f;

    void Start()
    {
        // �ʱ� ���ھ� ����
        UpdateScoreUI();
    }

    void Update()
    {
        // 1�ʸ��� ���ھ� ����
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            score += 1; // ���ھ� ����
            UpdateScoreUI(); // UI ������Ʈ
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }

        if(score > 1000)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // ���ھ� �ؽ�Ʈ ������Ʈ
        }
        else
        {
            Debug.LogWarning("Score TextMeshProUGUI�� �������� �ʾҽ��ϴ�!");
        }
    }
}
