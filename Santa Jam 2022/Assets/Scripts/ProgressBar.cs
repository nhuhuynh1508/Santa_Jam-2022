/*
    Also used as a glorified HP bar class.
    Very intense OOP programming.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    protected Slider _slider;
    private Player player;
    
    void Awake()
    {
        player = FindObjectOfType<Player>();
        _slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = player.currentHealth / player.maxHealth;
    }
}
