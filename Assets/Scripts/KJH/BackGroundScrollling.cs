using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경 스크롤을 구현하는 스크립트
public class BackGroundScrollling : MonoBehaviour
{
    [SerializeField] private Transform[] backgroundImages;
    [SerializeField] private float scrollSpeed;

    private Coroutine scrollCoroutine;

    private void Start()
    {
        StartBackgroundScroll();
    }

    private void StartBackgroundScroll()
    {
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }

        scrollCoroutine = StartCoroutine(BackgroundScrollCoroutine());
    }

    private IEnumerator BackgroundScrollCoroutine()
    {
        Vector3 scrollVec = new Vector3(scrollSpeed,0,0);
        while (true)
        {
            for (int i = 0; i < backgroundImages.Length; i++) // 배경화면 왼쪽으로 스크롤
            {
                backgroundImages[i].position -= scrollVec * Time.deltaTime;

                backgroundImages[i].position = new Vector3(
                Mathf.Round(backgroundImages[i].position.x * 1000f) / 1000f,
                backgroundImages[i].position.y,
                backgroundImages[i].position.z
                );
            }
            yield return null;
        }
    }

    public void CheckEnterTunnel()
    {
        foreach (Transform child in transform)
        {
            BackGroundTrigger childBackGroundTrigger = child.GetComponent<BackGroundTrigger>();
            if (childBackGroundTrigger != null)
            {
                childBackGroundTrigger.RunEnterTunnel();
            }
        }
    }

    public void CheckExitTunnel()
    {
        foreach (Transform child in transform)
        {
            BackGroundTrigger childBackGroundTrigger = child.GetComponent<BackGroundTrigger>();
            if (childBackGroundTrigger != null)
            {
                childBackGroundTrigger.RunExitTunnel();
            }
        }
    }
}
