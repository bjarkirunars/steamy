using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MeterScript healthMeter; //meter code
    private float timer = 0.0f;

    void Start()
    {
        healthMeter.SetMaxHealth(GameManager.instance.maxCoals); //meter code
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        healthMeter.SetHealth(GameManager.instance.currentCoals); //meter code
        if (
            timer > GameManager.instance.coalSpendTime && 
            GameManager.instance.currentCoals > 0 &&
            GameManager.instance.isTouchingGround)
        {
            GameManager.instance.currentCoals -= 1; //meter code
            timer = 0.0f;
        } else if (
            timer > GameManager.instance.coalSpendTime*2 && 
            GameManager.instance.currentCoals > 0 &&
            !GameManager.instance.isTouchingGround
        )
        {
            GameManager.instance.currentCoals -= 1; //meter code
            timer = 0.0f;
        }
    }
}
