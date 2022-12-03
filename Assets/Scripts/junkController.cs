using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class junkController : MonoBehaviour
{
    private int speedPenalty = 500;
    private int playerLayer = 13;

    private void OnCollisionEnter2D(Collision2D collision) {
            // Decrease car speed when car is colliding with junk
            if (collision.gameObject.layer == playerLayer) {
                GameManager.instance.carSpeed -=  speedPenalty ; // ADD (+ 1*grip)
                // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
                StartCoroutine(destroyer(collision));
            }
        }

    private void OnCollisionExit2D(Collision2D collision){
        // Increase car speed when junk is gone
        // if (collision.gameObject.name == "Car") {
        if (collision.gameObject.layer == playerLayer) {
            GameManager.instance.carSpeed += speedPenalty;
            // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
        }
    }

    IEnumerator destroyer(Collision2D collision)
    {
        yield return new WaitForSeconds(1);
        // Delay the collision disable by a second
        GetComponent<PolygonCollider2D>().enabled = false;
        // Disables collision between junk and the ground
        Destroy(this.gameObject, 2);
        // Destroys the game object after 2 seconds
    }
}
