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
    private bool isTouchingGroundR;
    private bool isTouchingGround;
    private bool isTouchingGroundL;
    public GameObject bigTire;
    public GameObject smallTire;
    //public wheelToGroundCheckR WTGCR = wheelToGroundCheckR();
    //public wheelToGroundCheckL WTGCL = wheelToGroundCheckL();
    
    public ParticleSystem frontSteamParticle;
    public ParticleSystem backSteamParticle;
    public ParticleSystem jumpSteamParticle;
    public ParticleSystem nitroSteamParticle;
    public ParticleSystem explosionParticle;

    public AudioSource steamAudio;
    public AudioSource steamNitroAudio;

    public GameObject gameOverScreen;
    public GameObject nitroText;
    public GameObject nitro1;
    public GameObject nitro2;
    public GameObject nitro3;
    public GameObject coalLabel;
    public TextMeshProUGUI currencyLabel;
    public TextMeshProUGUI coinLabel;
    // public GameObject awardScreen;
    // public TextMeshProUGUI awardLabel;
    JointMotor2D motorFront;

    JointMotor2D motorBack;
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;
    
    public AudioClip jumpSound;
    public AudioClip explosionSound;
    private double accumulativeCoal = 0;

    private int charges;
    private float motorOffTimer = 0.0f;

    private void Start() 
    {
        startX = Mathf.Abs(transform.position.x); 
        // Get starting position
        player = GetComponent<Rigidbody2D>();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
            coalLabel.SetActive(true);
        }
        CheckNitros();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.velocity.y < 0)
        {
            motorOffTimer += Time.deltaTime;
        } else {
            motorOffTimer = 0.0f;
        }
        if(GameManager.instance.nitroCharges > 0 && Input.GetKeyDown(KeyCode.N)){
            TriggerNitro();
        }
        if (GameManager.instance.currentCoals > 0 && GameManager.instance.gameRunning)
        {
            int carSpeed = GameManager.instance.maxCarSpeed;
            float axis = Input.GetAxisRaw("Horizontal");
            //isTouchingGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
            isTouchingGroundL = Physics2D.OverlapCircle(bigTire.transform.position,groundCheckRadius, groundLayer);
            isTouchingGroundR = Physics2D.OverlapCircle(smallTire.transform.position,groundCheckRadius,groundLayer);
            
            
            if (GameManager.instance.carSpeed < 0) 
                carSpeed = 0; 
            // Edge case for when speed is < 0 since car 
            // seemed to keep moving forward even with negative speed

            if (motorOffTimer > 0.8)
            {
                motorFront.motorSpeed = 0;
                motorFront.maxMotorTorque = 0;
                frontwheel.motor = motorFront;
                motorBack.motorSpeed = 0;
                motorBack.maxMotorTorque = 0;
                backwheel.motor = motorBack;
            } else
            {
                motorFront.motorSpeed = carSpeed * -1;
                motorFront.maxMotorTorque = 1000;
                frontwheel.motor = motorFront;
                motorBack.motorSpeed = carSpeed * -1;
                motorBack.maxMotorTorque = 1000;
                backwheel.motor = motorBack;
            }

            if (axis != 0) {
                steamAudio.Play();
                GetComponent<Rigidbody2D>().AddTorque(rotationSpeed * axis * -1);
                accumulativeCoal += 0.1;
                if (axis < 0) backSteamParticle.Emit(1);
                else frontSteamParticle.Emit(1);
            }

            if (Input.GetButtonDown("Jump") && (isTouchingGroundL||isTouchingGroundR) && GameManager.instance.jumpHeight > 0)
            {
                accumulativeCoal += 0.1;
                GameManager.instance.PlayClip(jumpSound);
                player.velocity = new Vector2(player.velocity.x, GameManager.instance.jumpHeight);
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
        if (player.position.x >= 1500)
        {
            Invoke("GoToWin", 2.0f);
        }
        if (accumulativeCoal >1)
        {
            GameManager.instance.currentCoals--;
            accumulativeCoal = 0;
        }
        // if (player.position.x > 10 )
        // {
        //     if(player.position.x < 15)
        //     {
        //     AwardScreen(10,50);
        //     }
        // }
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
        GameManager.instance.currency += GameManager.instance.coinCurrency;
        GameManager.instance.GameOver(currencyEarned, carExploded);
        Invoke("GameOverScreen", 1.5f);
        currencyLabel.text = "Distance:"+ currencyEarned.ToString() + " Meters" + 
            "\n\nYou earned: " + currencyEarned.ToString() + " Screws" +
            "\n\nAchievements: 0 Screws" +
            "\n\n Coins picked up: " + GameManager.instance.coinCurrency + " Screws";
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        coalLabel.SetActive(false);
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
    // void AwardScreen(int distance, int screws)
    // {
    //     awardScreen.SetActive(true);
    //     awardLabel.text = "You reached " + distance.ToString() +" meters." + "\n Screws earned: " + screws.ToString();
    //     Invoke("RemoveAwardScreen", 2f);
    // }

    void CheckNitros()
    {
        charges = GameManager.instance.nitroCharges;
        if (nitroText != null && nitro1 != null && nitro2 != null && nitro3 != null)
        {
            if (charges > 2)
            {
                nitroText.SetActive(true);
                nitro1.SetActive(true);
                nitro2.SetActive(true);
                nitro3.SetActive(true);
            } else if (charges > 1)
            {
                nitroText.SetActive(true);
                nitro1.SetActive(true);
                nitro2.SetActive(true);
                nitro3.SetActive(false);
            } else if (charges > 0)
            {
                nitroText.SetActive(true);
                nitro1.SetActive(true);
                nitro2.SetActive(false);
                nitro3.SetActive(false);
            } else
            {
                nitroText.SetActive(false);
                nitro1.SetActive(false);
                nitro2.SetActive(false);
                nitro3.SetActive(false);
            }
        }
    }

    void TriggerNitro() {
        GameManager.instance.maxCarSpeed += 4000;
        GameManager.instance.nitroCharges -= 1;
        steamNitroAudio.Play();
        nitroSteamParticle.Play();
        CheckNitros();
        Invoke("ResetSpeed", 2.0f);
    }

    void ResetSpeed() {
        GameManager.instance.maxCarSpeed -= 4000;
    }

    public void GotCoins()
    {
        coinLabel.text = "You got a coin\n +10 Screws!";
        Invoke("RemoveLabel", 2.0f);
    }

    void RemoveLabel()
    {
        coinLabel.text = "";
    }
    // void RemoveAwardScreen()
    // {
    //     awardScreen.SetActive(false);
    // }
}