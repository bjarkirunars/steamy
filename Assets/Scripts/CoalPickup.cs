using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour
{
    public AudioClip coalPickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "CarHitBox") {
            GameManager.instance.PlayClip(coalPickupSound);
            if (GameManager.instance.gameRunning) GameManager.instance.RefillCoal();
            // Prevents car from regaining fuel if Game Over has been achieved
            Destroy(gameObject);
        }
    }
}
