using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // "GameScene"�� ���� ���� �̸��Դϴ�.
        Debug.Log("���� ����!");
    }

    // ���� ���� ��ư
    public void ShowGameDescription()
    {
        SceneManager.LoadScene("DescriptionScene"); // "DescriptionScene"�� ���� ���� ���� �̸��Դϴ�.
        Debug.Log("���� ���� ����!");
    }
    public void QuitGame()
    {
        Debug.Log("���� ����!");
        Application.Quit(); // ���� ����
    }
}
