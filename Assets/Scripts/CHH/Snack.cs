using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : MonoBehaviour
{
    [SerializeField] GameObject snackPrefab;
    [SerializeField] int weight;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            snackPrefab.GetComponent<Rigidbody2D>().simulated = false;
        }
    }


    public int GetWeight()
    {
        return weight;
    }

    public void Consume()
    {
        // 스낵이 입에 저장될 때 비활성화
        Debug.Log("Snack consumed and stored!");
        gameObject.SetActive(false);
    }
    public void SpitOut(Vector3 position)
    {
        // 스낵을 뱉을 때 다시 활성화하고 위치 변경
        Debug.Log("Snack spit out!");
        gameObject.SetActive(true);
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
