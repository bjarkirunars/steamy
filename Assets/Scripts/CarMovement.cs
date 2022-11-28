using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    private int carSpeed;
    public float rotationSpeed;
    private int maxCoalAmount;
    private int currentCoal;
    // Start is called before the first frame update
    void Start()
    {
        carSpeed = GameManager.instance.carSpeed; // Get the car's current speed from the Game Manager
        maxCoalAmount = GameManager.instance.maxCoalAmount; // Get the starting amount of maximum coal from the Game Manager
        RefillCoal();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCoal > 0) {
            transform.Translate(Vector2.right * Time.deltaTime * carSpeed);
            if (Input.GetButton("Horizontal"))
            {
                transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            }
            int negCoal = (int) Time.fixedTime;
            currentCoal -= negCoal;
            // Debug.Log(negCoal);
            // Debug.Log(currentCoal);
        }
    }
    public void SetCoal(int newCoalAmount) {
        // Update the max amount of coal that the car can use
        maxCoalAmount = newCoalAmount;
    }

    public void RefillCoal() {
        // Refill the coal tank to full
        currentCoal = maxCoalAmount;
    }
}