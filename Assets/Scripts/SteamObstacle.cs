using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamObstacle : MonoBehaviour
{
    private int speedPenalty = 4;

    private void OnTriggerEnter2D(Collider2D other) {
        // Decrease car speed when car enters the steam obstacle
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "CarHitBox") {
            Debug.Log("Hello");
            GameManager.instance.carSpeed -= 100;
            // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        // Increase the car speed when the car exits the steam obstacle
        if (other.gameObject.tag == "CarHitBox") {
            GameManager.instance.carSpeed += speedPenalty;
            // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
        }    
    }
}
