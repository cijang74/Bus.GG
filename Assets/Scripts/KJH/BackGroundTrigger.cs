using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTrigger : MonoBehaviour
{
    private Vector3 movementVec;

    private void Awake()
    {
        movementVec = new Vector3(44f, transform.position.y, 0f); // 최신화할 위치 입력
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("범위 벗어남");
            transform.position = movementVec;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("범위 포함됨");
    }
}
