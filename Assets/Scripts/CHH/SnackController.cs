using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnackController : MonoBehaviour
{
    [SerializeField] Transform bus;
    [SerializeField] Transform mouse;
    [SerializeField] Transform[] seat;

    [SerializeField] GameObject snack1;
    [SerializeField] GameObject snack2;
    [SerializeField] GameObject snack3;
    [SerializeField] GameObject snack4;
    [SerializeField] GameObject snack5;
    [SerializeField] GameObject jam;

    int whatSnackNum;
    GameObject toSpawnSnack;

    float left;
    float right;
    float spawnGap;
    
    int leftRange;
    int rightRange;
    int arrayRange;

    float[] seatLeft;
    float[] seatRight;

    float minSpawnDelay = 1f;
    float maxSpawnDelay = 5f;


    bool[] canSpawnPoint;       //-8 ~ 8에 간격 1이라면 index 0번은 -8을 가리킴. 위치 - left = index


    void Start()
    {
        InitSpawnPointArray();
        StartCoroutine(SpawnTimer());
    }

    private int Calc(float a, float b)  //a를 b로 나눠서 나머지 버리고 int로 변환해주는 함수
    {
        return Convert.ToInt32(Math.Truncate(a / b));
    }

    private void InitSpawnPointArray()
    {
        spawnGap = mouse.localScale.x;                              //최소 스폰 간격은 쥐의 크기 (x좌표)
        left = bus.position.x - (bus.localScale.x / 2) + spawnGap;  //좌측 스폰 최대 길이 = 버스 좌측 좌표 + 최소 스폰 간격(우측으로 간격만큼)
        right = bus.position.x + (bus.localScale.x / 2) - spawnGap; //우측 스폰 최대 길이 = 버스 우측 좌표 - 최소 스폰 간격(좌측으로 간격만큼)

        leftRange = Calc(left, spawnGap);                           //0 기준 왼쪽에 떨어질 수 있는 좌표 수(음수)
        rightRange = Calc(right, spawnGap);                         //0 기준 우측에 떨어질 수 있는 좌표 수(양수)
        arrayRange = rightRange - leftRange + 1;                    //떨어질 수 있는 총 좌표 수

        canSpawnPoint = new bool[arrayRange];

        for(int i = 0; i < arrayRange; i++)
        {
            canSpawnPoint[i] = true;                                //모든 좌표에 떨어질 수 있게 초기화
        }

        CheckSeat();                                                //좌석 밑으로는 생성되지 않도록
    }

    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);//UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));    //1초부터 5초까지 랜덤으로 떨어짐
            SpawnSnack();
        }
    }

    public void CatchedSnack(GameObject obj)
    {
        int i = Calc(obj.transform.position.x , spawnGap) - leftRange;             
        canSpawnPoint[i] = true;                                    //간식을 집었으면 다시 그 위치에 떨어질 수 있음
    }

    private void SpawnSnack()
    {
        ChooseSpawnSnack();

        int arrayX;
        int count = 0;
        do
        {
            arrayX = UnityEngine.Random.Range(leftRange, rightRange + 1);       //가능한 좌표 중 랜덤으로 하나
            if(count++ > arrayRange)                                            //모든 가능한 좌표에 이미 간식이 있으면 안떨어짐
            {
                return;
            }
        }while(!canSpawnPoint[arrayX - leftRange]);                             //해당 좌표에 간식이 없으면
        canSpawnPoint[arrayX - leftRange] = false;                              //해당 좌표에 간식 있다고 표시

        Vector2 spawnPoint = new Vector2(arrayX * spawnGap, bus.position.y);                 

        Instantiate(toSpawnSnack, spawnPoint, Quaternion.identity);
    }

    private void CheckSeat()
    {
        int seatCount = seat.Length;                                //좌석의 갯수
        seatLeft = new float[seatCount];                    //좌석의 좌측좌표
        seatRight = new float[seatCount];                   //좌석의 우측좌표
        int seatLeftIndex;                                          //좌석의 좌측과 우측 사이에 가능한 좌표 Index중 제일 왼쪽거
        int seatRightIndex;
        for(int i = 0; i < seatCount; i++)
        {
            seatLeft[i] = seat[i].position.x - (seat[i].localScale.x / 2);
            seatRight[i] = seat[i].position.x + (seat[i].localScale.x / 2);
            seatLeftIndex = Calc(seatLeft[i] , spawnGap) - leftRange;
            seatRightIndex = Calc(seatRight[i] , spawnGap) - leftRange;
            
            for(int j = seatLeftIndex; j <= seatRightIndex; j++)
            {
                canSpawnPoint[j] = false;       //해당 좌석의 좌측과 우측 사이에 위치한 좌표는 못 떨어지도록
            }
        }
    }

    private void ChooseSpawnSnack()
    {
        int whatSnackNum = UnityEngine.Random.Range(1, 7);   //간식 1~5, 잼 중 랜덤
        if(whatSnackNum == 1)
        {
            toSpawnSnack = snack1;
        }
        else if(whatSnackNum == 2)
        {
            toSpawnSnack = snack2;
        }
        else if(whatSnackNum == 3)
        {
            toSpawnSnack = snack3;
        }
        else if(whatSnackNum == 4)
        {
            toSpawnSnack = snack4;
        }
        else if(whatSnackNum == 5)
        {
            toSpawnSnack = snack5;
        }
        else
        {
            toSpawnSnack = jam;
        }
    }

    public float[] GetSeatLeft()
    {
        return seatLeft;
    }

    public float[] GetSeatRight()
    {
        return seatRight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
