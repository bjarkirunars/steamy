using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentCoals > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * GameManager.instance.carSpeed);
        }
        if (Input.GetAxis("Horizontal") <0)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
        }
    }
    
    // public void SetCoal(int newCoalAmount) {
    //     // Update the max amount of coal that the car can use
    //     maxCoalAmount = newCoalAmount;
    // }

    public void RefillCoal() {
        // Refill the coal tank to full
        GameManager.instance.currentCoals += 10;
    }
}