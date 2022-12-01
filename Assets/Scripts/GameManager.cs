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
    public int maxCoals = 60;
    public int coalLevel;
    public int currentCoals = 60;
    public int currency;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currency = 0;
            coalLevel = 60;
            maxCarSpeed = carSpeed;
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

    public void GameOver(int currencyEarned) {
        if (gameRunning) {
            gameRunning = false;
            currency += currencyEarned;
        }
    }

    public void RestartGame() {
        gameRunning = true;
        carSpeed = maxCarSpeed;
        currentCoals = coalLevel;
    }

    public int GetCurrency() {
        return currency;
    }
    public void PlayClip(AudioClip clip) {
        // AudioSource audio = GameObject.FindObjectOfType<AudioSource>();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
    }
}
