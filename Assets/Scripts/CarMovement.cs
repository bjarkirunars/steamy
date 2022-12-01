using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float startX;
    private Rigidbody2D player;

    // public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    
    public ParticleSystem frontSteamParticle;
    public ParticleSystem backSteamParticle;
    public ParticleSystem jumpSteamParticle;

    public AudioSource steamAudio;


    public GameObject gameOverScreen;
    public TextMeshProUGUI currencyLabel;
    JointMotor2D motorFront;

    JointMotor2D motorBack;
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;
    
    public AudioClip jumpSound;

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
            int carSpeed = GameManager.instance.maxCarSpeed;
            float axis = Input.GetAxisRaw("Horizontal");
            isTouchingGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
            if (GameManager.instance.carSpeed < 0) carSpeed = 0; 
            // Edge case for when speed is < 0 since car 
            // seemed to keep moving forward even with negative speed

            //player.velocity = new Vector2(carSpeed, player.velocity.y);
            //if (Input.GetAxis("Horizontal") <0)
            //{
            //    transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            //    frontSteam.Play();
            //}
            //if (Input.GetAxis("Horizontal") > 0)
            //{
            //    transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
            //    backSteam.Play();
            //}
            motorFront.motorSpeed = carSpeed * -1;
            motorFront.maxMotorTorque = 1000;
            frontwheel.motor = motorFront;
            motorBack.motorSpeed = carSpeed * -1;
            motorBack.maxMotorTorque = 1000;
            backwheel.motor = motorBack;

            if (axis != 0) {
                steamAudio.Play();
                GetComponent<Rigidbody2D>().AddTorque(rotationSpeed * axis * -1);
                if (axis < 0) backSteamParticle.Play();
                else frontSteamParticle.Play();
            }

            if(Input.GetButtonDown("Jump") && isTouchingGround)
            {
                //transform.Translate(Vector2.up * Time.deltaTime * jumpSpeed);
                GameManager.instance.PlayClip(jumpSound);
                player.velocity = new   Vector2(player.velocity.x, GameManager.instance.jumpHeight);//*Time.deltaTime);
                jumpSteamParticle.Play();
            }
        }
        else if (player.velocity.x <= 0 && player.velocity.y <= 0) {
            int currencyEarned = CalculateCurrency();
            GameManager.instance.GameOver(currencyEarned);
            gameOverScreen.SetActive(true);
            currencyLabel.text = "You earned: " + currencyEarned.ToString() + " Screws";
        } else {
            motorFront.motorSpeed = 0;
            motorFront.maxMotorTorque = 0;
            frontwheel.motor = motorFront;
            motorBack.motorSpeed = 0;
            motorBack.maxMotorTorque = 0;
            backwheel.motor = motorBack;
        }
    }

    public void RefillCoal() {
        // Refill the coal tank to full
        GameManager.instance.currentCoals += 20;
    }

    public int CalculateCurrency() {
        float endX = Mathf.Abs(transform.position.x);
        float totalDistance = startX + endX;
        return (int) totalDistance;
    }

}