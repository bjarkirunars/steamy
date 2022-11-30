using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float startX;
    public float jumpSpeed;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    void Start() {
        startX = Mathf.Abs(transform.position.x);
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentCoals > 0)
        {
            int carSpeed = GameManager.instance.carSpeed;
            if (GameManager.instance.carSpeed < 0) carSpeed = 0; 
            // Edge case for when speed is < 0 since car 
            // seemed to keep moving forward even with negative speed

            isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,groundLayer);
            player.velocity = new   Vector2(carSpeed, player.velocity.y);
            if (Input.GetAxis("Horizontal") <0)
            {
                transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
            }

            if(Input.GetButtonDown("Jump") && isTouchingGround)
            {
                //transform.Translate(Vector2.up * Time.deltaTime * jumpSpeed);
                player.velocity = new   Vector2(player.velocity.x, jumpSpeed);//*Time.deltaTime);
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