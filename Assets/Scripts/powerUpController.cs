using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{
    public AudioClip screwPickupSound;

    private void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("Car detected" + GameManager.instance.carSpeed.ToString());

        if (collision.gameObject.layer == 13) {
            GameManager.instance.PlayClip(screwPickupSound);
            GameManager.instance.currency +=  10 ;
            Destroy(gameObject);
        }
}
}
