using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamObstacle : MonoBehaviour
{
    private int speedPenalty = 500;

    private void OnTriggerEnter2D(Collider2D other) {
        // Decrease car speed when car enters the steam obstacle
        if (other.gameObject.name == "Car") {
            GameManager.instance.carSpeed -= speedPenalty;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Increase the car speed when the car exits the steam obstacle
        if (other.gameObject.name == "Car") {
            GameManager.instance.carSpeed += speedPenalty;
        }    
    }
}
