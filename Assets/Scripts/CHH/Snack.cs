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
        // ������ �Կ� ����� �� ��Ȱ��ȭ
        Debug.Log("Snack consumed and stored!");
        gameObject.SetActive(false);
    }
    public void SpitOut(Vector3 position)
    {
        // ������ ���� �� �ٽ� Ȱ��ȭ�ϰ� ��ġ ����
        Debug.Log("Snack spit out!");
        gameObject.SetActive(true);
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
