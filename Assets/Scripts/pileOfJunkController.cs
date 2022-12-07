using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pileOfJunkController : MonoBehaviour
{
    public AudioClip pileOfJunkSound;

    private void OnCollisionEnter2D(Collision2D collision) {
            // Decrease car speed when car is colliding with junk
            

            if (collision.gameObject.layer == 13) {
                Debug.Log("Car detected");
                StartCoroutine(destroyer(collision));
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
