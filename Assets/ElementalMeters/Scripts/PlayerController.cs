﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MeterScript healthMeter; //meter code
    public int currentHealth; //meter code
    public int maxHealth = 80; //meter code
    private float timer = 0.0f;
    private float waitTime = 2.0f;

    void Start()
    {
        healthMeter.SetMaxHealth(maxHealth); //meter code
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        healthMeter.SetHealth(GameManager.instance.currentCoals); //meter code
        if (timer > waitTime)
        {
            GameManager.instance.currentCoals -= 5; //meter code
            timer = 0.0f;
        }
    }
}