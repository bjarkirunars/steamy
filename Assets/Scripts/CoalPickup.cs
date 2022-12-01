using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour
{
    public AudioClip coalPickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        GameManager.instance.PlayClip(coalPickupSound);
        GameManager.instance.RefillCoal();
        Destroy(gameObject);
    }
}
