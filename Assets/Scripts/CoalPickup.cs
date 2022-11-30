using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GameManager.instance.RefillCoal();
        Destroy(gameObject);
    }
}
