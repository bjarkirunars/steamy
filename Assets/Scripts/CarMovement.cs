using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float startX;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    
    public ParticleSystem frontSteamParticle;
    public ParticleSystem backSteamParticle;

    public AudioSource frontSteamAudio;
    public AudioSource backSteamAudio;


    public GameObject gameOverScreen;
    public TextMeshProUGUI currencyLabel;

    private void Start() 
    {
        startX = Mathf.Abs(transform.position.x); 
        // Get starting position
        player = GetComponent<Rigidbody2D>();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentCoals > 0)
        {
            float rotationAngle;
            int carSpeed = GameManager.instance.carSpeed;
            float axis = Input.GetAxis("Horizontal");
            // Debug.Log("Axis: " + axis.ToString());
            if (GameManager.instance.carSpeed < 0) carSpeed = 0; 
            // Edge case for when speed is < 0 since car 
            // seemed to keep moving forward even with negative speed

            isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,groundLayer);
            player.velocity = new Vector2(carSpeed, player.velocity.y);
            if (axis < 0) {
            // Left arrow or A is being pressed/held
                rotationAngle = rotationSpeed * Time.fixedDeltaTime; // * axis;
                
                Debug.Log("Left arrow is being held: " + rotationAngle.ToString()); 
                transform.Rotate(0, 0, rotationAngle, Space.Self);
                frontSteamParticle.Play();
                frontSteamAudio.Play();
            }

            if (axis > 0) {
            // Right arrow or D is being pressed/held
                rotationAngle = -rotationSpeed * Time.fixedDeltaTime; // * axis;
                Debug.Log("Right arrow is being held: " + rotationAngle.ToString());
                transform.Rotate(0, 0, rotationAngle, Space.Self);
                backSteamParticle.Play();
                backSteamAudio.Play();
            }

            if(Input.GetButtonDown("Jump") && isTouchingGround)
            {
                //transform.Translate(Vector2.up * Time.deltaTime * jumpSpeed);
                player.velocity = new Vector2(player.velocity.x, GameManager.instance.jumpHeight);//*Time.deltaTime);
            }
        }
        else {
            if (player.velocity.x == 0 && player.velocity.y == 0) {
                int currencyEarned = CalculateCurrency();
                GameManager.instance.GameOver(currencyEarned);
                gameOverScreen.SetActive(true);
                currencyLabel.text = "You earned: " + currencyEarned.ToString() + " Screws";
            }
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