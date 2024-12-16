using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioSource audioSource3;
    [SerializeField] AudioClip jam;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip eat;

    public void PlayCrashSound()
    {
        if (crash != null)
        {
            audioSource1.clip = crash;
            audioSource1.time = 0.3f;
            audioSource1.Play();
        }
    }
    public void PlayJamSound()
    {
        if (jam != null)
        {
            audioSource2.clip = jam;
            audioSource2.time = 0.3f;
            audioSource2.Play();
        }
    }
    public void PlayEatSound()
    {
        if (eat != null)
        {
            audioSource3.clip = eat;
            audioSource3.time = 0.5f;
            audioSource3.Play();
        }
    }
}
