using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour
{
    public AudioSource audioManager;

    private void OnTriggerEnter2D(Collider2D other) {
        audioManager.Play(); 
        // FIXME: Audio doesn't play since powerup is destroyed too quickly
        GameManager.instance.RefillCoal();
        Destroy(gameObject);
    }
}
