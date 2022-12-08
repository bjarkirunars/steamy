using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{
    public AudioClip coinPickupSound;
    public CarMovement car;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Car") {
            car = other.gameObject.GetComponent<CarMovement>();
            GameManager.instance.PlayClip(coinPickupSound);
            GameManager.instance.coinCurrency +=  10;
            if(car != null)
            {
                car.GotCoins();
            }
            Destroy(gameObject);
        }
}
}
