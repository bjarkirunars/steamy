using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameRunning = true;
    public int maxCarSpeed = 1;
    public int carSpeed = 1;
    public int jumpHeight = 0;
    public int nitroCharges = 0;
    public int maxCoals = 60;
    public int speedLevel = 1;
    public int jumpLevel = 1;
    public int coalUpgradeLevel = 1;
    public int coalLevel = 1;
    public int nitroLevel = 1;
    public int currentCoals = 60;
    public float coalSpendTime = 0.2f;
    public int currency;
    public int coinCurrency;
    public int achievementCurrency;
    public AudioClip gameOverClip;
    public int maxDistance;
    public bool Acivement1 = false;
    public bool Acivement2 = false;
    public bool Acivement3 = false;
    public bool Acivement4 = false;
    public bool Acivement5 = false;
    public bool Insturctions = false;
    public bool isTouchingGround = true;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currency = 0;
            coinCurrency = 0;
            achievementCurrency = 0;
            coalLevel = 60;
            maxCarSpeed = carSpeed;
            nitroCharges = nitroLevel - 1;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void RefillCoal() {
        CarMovement car = GameObject.Find("Car").GetComponent<CarMovement>();
        // Make sure to rename the Speedpunk GameObject to "Car"
        car.RefillCoal();
    }

    private void FixedUpdate()
    {
        if (currentCoals > maxCoals) 
        { // If Coal refill powerUp exceeds max coal capacity
            currentCoals = maxCoals;
        }
        if (currentCoals <= 0)
        { // If car runs out of coal
            carSpeed = 0;
        }
    }

    public void GameOver(int currencyEarned, bool carExploded = false) {
        if (gameRunning) {
            if (!carExploded) {
                PlayClip(gameOverClip);
            }
            gameRunning = false;
            currency += currencyEarned;
            if (currencyEarned > maxDistance)
            {
                maxDistance = currencyEarned;
            }
        }
    }

    public void RestartGame() {
        gameRunning = true;
        carSpeed = maxCarSpeed;
        currentCoals = coalLevel;
        nitroCharges = nitroLevel - 1;
    }

    public void ResetGame() {
            maxCarSpeed = 1000;
            carSpeed = 1000;
            jumpHeight = 0;
            maxCoals = 60;
            coalLevel = 60;
            currentCoals = 60;
            currency = 0;
            coinCurrency = 0;
            achievementCurrency = 0;
            nitroCharges = 0;
            speedLevel = 1;
            jumpLevel = 1;
            coalUpgradeLevel = 1;
            nitroLevel = 1;
            coalSpendTime = 0.2f;
            currency = 0;
            maxDistance = 0;
            Acivement1 = false;
            Acivement2 = false;
            Acivement3 = false;
            Acivement4 = false;
            Acivement5 = false;
    }

    public int GetCurrency() {
        return currency;
    }
    public void PlayClip(AudioClip clip) {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (AudioListener.volume == 0) {
                AudioListener.volume = 1;
            } else {
                AudioListener.volume = 0;
            }
        }
        
    }
}
