using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // "GameScene"은 게임 씬의 이름입니다.
        Debug.Log("게임 시작!");
    }

    // 게임 설명 버튼
    public void ShowGameDescription()
    {
        SceneManager.LoadScene("DescriptionScene"); // "DescriptionScene"은 게임 설명 씬의 이름입니다.
        Debug.Log("게임 설명 보기!");
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료!");
        Application.Quit(); // 게임 종료
    }
}
