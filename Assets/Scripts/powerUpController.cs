using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{
    public AudioClip coinPickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());

        if (other.gameObject.tag == "CarHitBox") {
            GameManager.instance.PlayClip(coinPickupSound);
            GameManager.instance.currency +=  10 ;
            Destroy(gameObject);
        }
}
}
