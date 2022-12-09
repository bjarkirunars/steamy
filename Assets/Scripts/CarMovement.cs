using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CarMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float startX;
    private Rigidbody2D player;
    // public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGroundR;
    private bool isTouchingGroundL;
    private GameObject bigTire;
    private GameObject smallTire;
    //public wheelToGroundCheckR WTGCR = wheelToGroundCheckR();
    //public wheelToGroundCheckL WTGCL = wheelToGroundCheckL();
    
    public ParticleSystem frontSteamParticle;
    public ParticleSystem backSteamParticle;
    public ParticleSystem jumpSteamParticle;
    public ParticleSystem nitroSteamParticle;
    public ParticleSystem explosionParticle;

    public AudioSource steamAudio;
    public AudioSource steamNitroAudio;

    private GameObject gameOverScreen;
    private GameObject nitroText;
    private GameObject nitro1;
    private GameObject nitro2;
    private GameObject nitro3;
    private GameObject overlayCanvas;
    public TextMeshProUGUI currencyLabel;
    public TextMeshProUGUI coinLabel;
    public TextMeshProUGUI distanceLabel;
    // public GameObject awardScreen;
    public TextMeshProUGUI awardLabel;
    JointMotor2D motorFront;

    JointMotor2D motorBack;
    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;
    
    public AudioClip jumpSound;
    public AudioClip explosionSound;
    private double accumulativeCoal = 0;

    private int charges;
    private CinemachineVirtualCamera camObj;
    private float cameraTarget;
    private float cameraCurrent = 6f;
    private float cameraMaxDelta = 0.01f;
    private Vector2 velo;


    private void Start() 
    {
        camObj = GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        nitroText = GameObject.Find("NitroText");
        nitro1 = GameObject.Find("Nitro1");
        nitro2 = GameObject.Find("Nitro2");
        nitro3 = GameObject.Find("Nitro3");
        gameOverScreen = GameObject.Find("GameOverScreen");
        overlayCanvas = GameObject.Find("CanvasOverlay");
        bigTire = GameObject.Find("TyreBigB_copy");
        smallTire = GameObject.Find("TyreSmallF_copy");
        startX = Mathf.Abs(transform.position.x); 
        // Get starting position
        player = GetComponent<Rigidbody2D>();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
            overlayCanvas.SetActive(true);
        }
        CheckNitros();
    }

    // Update is called once per frame
    void Update()
    {
        float endX = transform.position.x;
        float totalDistance = endX + startX;
        if (distanceLabel != null) {distanceLabel.text = (int)totalDistance + "KM / 1360KM";}
        
        if(GameManager.instance.nitroCharges > 0 && Input.GetKeyDown(KeyCode.N)){
            TriggerNitro();
        }

        if (GameManager.instance.currentCoals > 0 && GameManager.instance.gameRunning)
        {
            int carSpeed = GameManager.instance.maxCarSpeed;
            float axis = Input.GetAxisRaw("Horizontal");
            isTouchingGroundL = Physics2D.OverlapCircle(bigTire.transform.position,groundCheckRadius, groundLayer);
            isTouchingGroundR = Physics2D.OverlapCircle(smallTire.transform.position,groundCheckRadius,groundLayer);
            GameManager.instance.isTouchingGround = isTouchingGroundL || isTouchingGroundR;
            
            if (GameManager.instance.carSpeed < 0) 
                carSpeed = 0; 
            // Edge case for when speed is < 0 since car 
            // seemed to keep moving forward even with negative speed
            if (player.velocity.x < 6) 
            {cameraTarget = 6;} else if (player.velocity.x > 10)
            {cameraTarget = 10;} else {cameraTarget = player.velocity.x;}
            int negOrPos = (cameraTarget - cameraCurrent) < 0 ? -1 : 1;
            cameraCurrent = cameraCurrent + cameraMaxDelta * negOrPos;
            camObj.m_Lens.OrthographicSize = cameraCurrent;
            if (player.velocity.x > carSpeed/200)
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
                motorFront.maxMotorTorque = 10000;
                frontwheel.motor = motorFront;
                motorBack.motorSpeed = carSpeed * -1;
                motorBack.maxMotorTorque = 10000;
                backwheel.motor = motorBack;
            }

            if (axis != 0) {
                steamAudio.Play();
                GetComponent<Rigidbody2D>().AddTorque(rotationSpeed * axis * -1);
                accumulativeCoal += (0.1 - (GameManager.instance.coalUpgradeLevel - 1) / 150);
                if (axis < 0) backSteamParticle.Emit(1);
                else frontSteamParticle.Emit(1);
            }

            if (Input.GetButtonDown("Jump") && (GameManager.instance.isTouchingGround) && GameManager.instance.jumpHeight > 0)
            {
                accumulativeCoal += (0.1 - (GameManager.instance.coalUpgradeLevel - 1) / 150);
                GameManager.instance.PlayClip(jumpSound);
                velo = new Vector2(player.velocity.x,player.velocity.y).normalized;

                if (player.rotation < 0)
                {
                player.velocity = new Vector2(-velo.y * GameManager.instance.jumpHeight, velo.x * GameManager.instance.jumpHeight) ;
                    //player.velocity = new Vector2(player.velocity.y * GameManager.instance.jumpHeight * -1, player.velocity.x * GameManager.instance.jumpHeight);
                }
                else
                {
                player.velocity = new Vector2(velo.y * GameManager.instance.jumpHeight, velo.x * GameManager.instance.jumpHeight) ;
                    //player.velocity = new Vector2(player.velocity.y * GameManager.instance.jumpHeight, player.velocity.x * GameManager.instance.jumpHeight);

                }
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
        if (player.position.x >= 1360)
        {
            Invoke("GoToWin", 1.0f);
        }
        if (accumulativeCoal >1)
        {
            GameManager.instance.currentCoals--;
            accumulativeCoal = 0;
            if (!GameManager.instance.Insturctions)
            {
                awardLabel.text = "Whatch Out tilting and jumping spend coals";
                Invoke("RemoveAwardScreen" , 5f);
                GameManager.instance.Insturctions = true;
            }
        }
        if (totalDistance > 100)
        {
            if (!GameManager.instance.Acivement1)
            {
                AwardScreen(100, 100);
                GameManager.instance.achievementCurrency += 100;
                GameManager.instance.Acivement1 = true;
            }
        }
        if (totalDistance > 350)
        {
            if (!GameManager.instance.Acivement2)
            {
                AwardScreen(350, 500);
                GameManager.instance.achievementCurrency += 500;
                GameManager.instance.Acivement2 = true;
            }
        }
        if (totalDistance > 500)
        {
            if (!GameManager.instance.Acivement3)
            {
                AwardScreen(500, 750);
                GameManager.instance.achievementCurrency += 750;
                GameManager.instance.Acivement3 = true;
            }
        }
        if (totalDistance > 750)
        {
            if (!GameManager.instance.Acivement4)
            {
                AwardScreen(750, 1000);
                GameManager.instance.achievementCurrency += 1000;
                GameManager.instance.Acivement4 = true;
            }
        }
        if (totalDistance > 1000)
        {
            if (!GameManager.instance.Acivement5)
            {
                AwardScreen(1000, 3000);
                GameManager.instance.achievementCurrency += 10000;
                GameManager.instance.Acivement5 = true;
            }
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
        GameManager.instance.currency += GameManager.instance.coinCurrency;
        GameManager.instance.currency += GameManager.instance.achievementCurrency;
        GameManager.instance.GameOver(currencyEarned, carExploded);
        Invoke("GameOverScreen", 1.5f);
        currencyLabel.text = "Distance:"+ currencyEarned.ToString() + " KMs" + 
            "\n\nAchievements: " + GameManager.instance.achievementCurrency + " Screws" +
            "\n\n Coins picked up: " + GameManager.instance.coinCurrency + " Screws" +
            "\n\nYou earned: " + (currencyEarned + GameManager.instance.coinCurrency + GameManager.instance.achievementCurrency).ToString() + " Screws";
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        overlayCanvas.SetActive(false);
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
    void AwardScreen(int distance, int screws)
    {
        //awardScreen.SetActive(true);
        awardLabel.text = "You reached " + distance.ToString() + " meters." + " Screws earned: " + screws.ToString();
        Invoke("RemoveAwardScreen", 2f);
    }

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
    void RemoveAwardScreen()
    {
        awardLabel.text = "";
    }
}