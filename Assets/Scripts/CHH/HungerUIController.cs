using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerUIController : MonoBehaviour
{
    [SerializeField] Slider hungerSlider;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        hungerSlider.value = player.GetComponent<Player>().GetFull();
    }
}
