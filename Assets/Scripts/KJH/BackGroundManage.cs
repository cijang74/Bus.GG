using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManage : MonoBehaviour
{
    [SerializeField] public bool isEnterTunnel = false;
    [SerializeField] public bool isExitTunnel = false;

    private void FixedUpdate() 
    {
        ActiveEnterTunnel();
        ActiveExitTunnel();
    }

    public void ActiveEnterTunnel()
    {
        if(isEnterTunnel)
        {
            foreach (Transform child in transform)
            {
                BackGroundScrollling childBackGroundScrollling = child.GetComponent<BackGroundScrollling>();
                if (childBackGroundScrollling != null)
                {
                    childBackGroundScrollling.CheckEnterTunnel();
                }
            }
        }
    }

    public void ActiveExitTunnel()
    {
        if(isExitTunnel)
        {
            foreach (Transform child in transform)
            {
                BackGroundScrollling childBackGroundScrollling = child.GetComponent<BackGroundScrollling>();
                if (childBackGroundScrollling != null)
                {
                    childBackGroundScrollling.CheckExitTunnel();
                }
            }
        }
    }
}
