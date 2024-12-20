using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInstance : MonoBehaviour
{
    void Start()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Manager") || obj.CompareTag("Player")) // Ensure you assign the tag in the Editor
            {
                Destroy(obj);
            }
        }
    }
}
