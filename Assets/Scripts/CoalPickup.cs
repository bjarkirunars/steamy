using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision detected");
        //CarMovement car = other.GetComponent<CarMovement>();
        //car.RefillCoal();
        GameManager.instance.RefillCoal();
        Destroy(gameObject);
    }
}
