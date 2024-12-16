using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertEnvironment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "TunnelEnterence" && TunnelManager.Instance.isMakingTunnel)
        {
            UIFade.Instance.FadeToBlack();
            TunnelManager.Instance.RevertEnvironment();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "OutSide" && !TunnelManager.Instance.isMakingTunnel)
        {
            UIFade.Instance.FadeToClear();
            TunnelManager.Instance.RevertEnvironment();
        }
    }
}
