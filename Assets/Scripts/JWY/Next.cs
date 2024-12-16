using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void NextDe()
    {
        SceneManager.LoadScene("DescriptionScene 1"); // "GameScene"은 게임 씬의 이름입니다.
        Debug.Log("게임 시작!");
    }
}
