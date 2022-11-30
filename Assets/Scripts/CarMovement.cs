using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float startX;
    public Rigidbody2D car;
    public Vector2 direction;
    public float impulse;
    public ParticleSystem frontSteam;
    public ParticleSystem backSteam;

    private void Start() {
        startX = Mathf.Abs(transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentCoals > 0)
        {
            car.velocity = direction.normalized * impulse;
            //transform.Translate(Vector2.right * Time.deltaTime * GameManager.instance.carSpeed);
            if (Input.GetAxis("Horizontal") <0)
            {
                transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
                frontSteam.Play();
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
                backSteam.Play();
            }
        }
        else {
            int currencyEarned = CalculateCurrency();
            GameManager.instance.GameOver(currencyEarned);
        }
    }

    public void RefillCoal() {
        // Refill the coal tank to full
        GameManager.instance.currentCoals += 10;
    }

    public int CalculateCurrency() {
        float endX = Mathf.Abs(transform.position.x);
        float totalDistance = startX + endX;
        return (int) totalDistance;
    }

}