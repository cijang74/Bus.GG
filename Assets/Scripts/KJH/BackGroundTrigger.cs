using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTrigger : MonoBehaviour
{
    private Vector3 startMovementVec;
    private Vector3 movementVec;

    private void Awake()
    {
        startMovementVec = new Vector3(44f, transform.position.y, transform.position.z); // 최신화할 위치 입력
        movementVec = startMovementVec;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            //Debug.Log("범위 벗어남");
            transform.position = movementVec;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        //Debug.Log("범위 포함됨");
    }

    public void RunEnterTunnel() // 터널 들어갔을 때
    {
        if(gameObject.tag == "OutSide")
        {
            movementVec = new Vector3(44f, transform.position.y, 10f);
        }

        if(gameObject.tag == "Tunnel")
        {
            movementVec = new Vector3(44f, transform.position.y, 0f);
        }
    }

    public void RunExitTunnel() // 터널 나갔을 때
    {
        movementVec = startMovementVec;
    }
}
