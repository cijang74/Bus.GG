using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnackController : MonoBehaviour
{
    [SerializeField] GameObject snack1;
    [SerializeField] GameObject snack2;
    [SerializeField] GameObject snack3;
    [SerializeField] GameObject snack4;
    [SerializeField] GameObject snack5;
    [SerializeField] GameObject jam;

    int whatSnackNum;
    GameObject toSpawnSnack;

    float left = -8.0f;
    float right = 8.0f;
    float spawnGap = 1.0f;
    
    int leftRange;
    int rightRange;
    int arrayRange;

    float minSpawnDelay = 1f;
    float maxSpawnDelay = 5f;


    bool[] canSpawnPoint;       //-8 ~ 8이라면 index 0번은 -8을 가리킴. 위치 - left = index


    void Start()
    {
        InitSpawnPointArray();
        StartCoroutine(SpawnTimer());
    }

    private void InitSpawnPointArray()
    {
        leftRange = Convert.ToInt32(Math.Truncate(left / spawnGap));
        rightRange = Convert.ToInt32(Math.Truncate(right / spawnGap));
        arrayRange = rightRange - leftRange + 1;

        canSpawnPoint = new bool[arrayRange];

        for(int i = 0; i < arrayRange; i++)
        {
            canSpawnPoint[i] = true;
        }
    }

    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnSnack();
        }
    }

    private void SpawnSnack()
    {
        ChooseSpawnSnack();

        int arrayX;
        int count = 0;
        do
        {
            arrayX = UnityEngine.Random.Range(leftRange, rightRange + 1);
            if(count++ > arrayRange)
            {
                return;
            }
        }while(!canSpawnPoint[arrayX - leftRange]);
        canSpawnPoint[arrayX - leftRange] = false;

        Vector2 spawnPoint = new Vector2(arrayX * spawnGap, 0);

        Instantiate(toSpawnSnack, spawnPoint, Quaternion.identity);
    }

    private void ChooseSpawnSnack()
    {
        int whatSnackNum = UnityEngine.Random.Range(1,6);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
