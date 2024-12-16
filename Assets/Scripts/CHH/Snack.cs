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

    // Update is called once per frame
    void Update()
    {
        
    }
}
