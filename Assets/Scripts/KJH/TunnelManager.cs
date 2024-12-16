using System.Collections;
using UnityEngine;

public class TunnelManager : Singleton<TunnelManager>
{
    public bool isMakingTunnel = false; // 터널 상태 여부
    private float waitTime = 0f; // 대기 시간
    private Coroutine executeCoroutine;
    private Coroutine printCoroutine;
    private BackGroundManage backGroundManage;

    protected override void Awake() 
    {
        base.Awake();
        backGroundManage = FindAnyObjectByType<BackGroundManage>();
    }

    private void Start()
    {
        // 초기 코루틴 실행
        executeCoroutine = StartCoroutine(ExecuteAfterRandomTime());
        printCoroutine = StartCoroutine(PrintTime());
    }

    private void RefreshRoutine()
    {
        // 실행 중인 코루틴을 정리
        if (executeCoroutine != null)
        {
            StopCoroutine(executeCoroutine);
        }
        if (printCoroutine != null)
        {
            StopCoroutine(printCoroutine);
        }

        // 코루틴을 다시 시작
        executeCoroutine = StartCoroutine(ExecuteAfterRandomTime());
        printCoroutine = StartCoroutine(PrintTime());
    }

    private IEnumerator ExecuteAfterRandomTime()
    {
        // 터널 상태에 따라 랜덤 시간 설정
        if (isMakingTunnel)
        {
            waitTime = Random.Range(15f, 20f);
        }
        else
        {
            waitTime = Random.Range(5f, 10f);
        }

        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(waitTime);

        // 상태 전환 및 로그 출력
        isMakingTunnel = !isMakingTunnel;
        RevertEnvironment();
        Debug.Log("전환");

        // 루틴 새로고침
        RefreshRoutine();
    }

    private IEnumerator PrintTime()
    {
        float remainingTime = waitTime;

        while (remainingTime > 0)
        {
            Debug.Log($"{remainingTime:F1}초 남음..");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }

    public void RevertEnvironment()
    {
        if(isMakingTunnel)
        {
            backGroundManage.isEnterTunnel = true;
            backGroundManage.isExitTunnel = false;
        }

        else
        {
            backGroundManage.isEnterTunnel = false;
            backGroundManage.isExitTunnel = true;
        }
    }
}
