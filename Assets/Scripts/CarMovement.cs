using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public ParticleSystem nitroSteamParticle;
    public ParticleSystem explosionParticle;

    public AudioSource steamAudio;
    public AudioSource steamNitroAudio;

    private Vector2 m_NewForce;


    public GameObject gameOverScreen;
    public TextMeshProUGUI currencyLabel;
    JointMotor2D motorFront;

    JointMotor2D motorBack;
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;
    
    public AudioClip jumpSound;
    public AudioClip explosionSound;
    private double accumulativeCoal = 0;

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
        if(Input.GetKeyDown(KeyCode.N)){
            TriggerNitro();
        }
        if (GameManager.instance.currentCoals > 0 && GameManager.instance.gameRunning)
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
                accumulativeCoal += 0.1;
                if (axis < 0) backSteamParticle.Emit(1);
                else frontSteamParticle.Emit(1);
            }

            if(Input.GetButtonDown("Jump") && isTouchingGround && GameManager.instance.jumpHeight > 0)
            {
                //transform.Translate(Vector2.up * Time.deltaTime * jumpSpeed);
                accumulativeCoal += 0.1;
                GameManager.instance.PlayClip(jumpSound);
                player.velocity = new Vector2(player.velocity.x, GameManager.instance.jumpHeight);//*Time.deltaTime);
                jumpSteamParticle.Play();
            }
        }
        else if (player.velocity.x <= 0 && player.velocity.y <= 0 && GameManager.instance.gameRunning) {
            EndGame();
        } 
        else {
            motorFront.motorSpeed = 0;
            motorFront.maxMotorTorque = 0;
            frontwheel.motor = motorFront;
            motorBack.motorSpeed = 0;
            motorBack.maxMotorTorque = 0;
            backwheel.motor = motorBack;
        }
        if (player.position.x >= 460)
        {
            Invoke("GoToWin", 2.0f);
        }
        if (accumulativeCoal >1)
        {
            GameManager.instance.currentCoals--;
            accumulativeCoal = 0;
        }
    }

    void GoToWin()
    {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Winner");
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

    void EndGame(bool carExploded = false) {
        // Car exploded boolean is to choose correct game over
        // sound based on whether car ran out of fuel or exploded
        int currencyEarned = CalculateCurrency();
        GameManager.instance.GameOver(currencyEarned, carExploded);
        gameOverScreen.SetActive(true);
        currencyLabel.text = "You earned: " + currencyEarned.ToString() + " Screws";
    }

    void OnTriggerEnter2D(Collider2D other) {
        int groundLayer = 6;
        if (GameManager.instance.gameRunning && other.gameObject.layer == groundLayer) {
        // If car lands upside down on the ground
            GameManager.instance.PlayClip(explosionSound);
            explosionParticle.Play();
            // GameManager.instance.currentCoals = 0;
            EndGame(true);
        }
    }

    void TriggerNitro() {
        GameManager.instance.maxCarSpeed += 4000;
        steamNitroAudio.Play();
        nitroSteamParticle.Play();
        Invoke("ResetSpeed", 2.0f);
    }

    void ResetSpeed() {
        GameManager.instance.maxCarSpeed -= 4000;
    }
}