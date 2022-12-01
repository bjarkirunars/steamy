using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkController : MonoBehaviour
{

    public int penalityWeight;

 
    private void OnCollisionEnter2D(Collision2D collision) {
            // Decrease car speed when car is colliding with junk

            if (collision.gameObject.name == "Car") {
                GameManager.instance.carSpeed -= penalityWeight;
                Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
                StartCoroutine(destroyer(collision));
            }
        }
    private void OnCollisionExit2D(Collision2D collision){
        // Increase car speed when junk is gone
        if (collision.gameObject.name == "Car") {
                GameManager.instance.carSpeed += penalityWeight;
                Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());
                
            }
    }
    IEnumerator destroyer(Collision2D collision)
    {
        
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

   
}
