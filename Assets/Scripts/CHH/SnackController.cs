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

    int whatSnackNum;
    GameObject toSpawnSnack;

    float left = -8.0f;
    float right = 8.0f;
    float minSpawnDelay = 1f;
    float maxSpawnDelay = 5f;

    float timer = 0f;

    //bool canSpawnPoint[right - left + 1];       //-8 ~ 8이라면 index 0번은 -8을 가리킴. 위치 - left = index


    void Start()
    {
        InitSpawnPointArray();
        StartCoroutine(SpawnTimer());
        StartCoroutine(Timer());
    }

    private void InitSpawnPointArray()
    {
        int arrayRange = Convert.ToInt32(right - left + 1);
        for(int i = 0; i < arrayRange; i++)
        {
            //canSpawnPoint[i] = true;
        }
    }
    
    private IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
            Debug.Log(Math.Round(timer, 1));
        }        
    }

    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            timer = 0f;
            SpawnSnack();
        }
    }

    private void SpawnSnack()
    {
        ChooseSpawnSnack();
        Vector2 spawnPoint = new Vector2(UnityEngine.Random.Range(left, right), 0);

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
        else
        {
            toSpawnSnack = snack5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
