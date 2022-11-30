using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // public GameObject gameOverScreen;
    // public TextMeshProUGUI currencyLabel;
    private bool gameRunning = true;
    public int maxCarSpeed = 1;
    public int carSpeed = 1;
    public int maxCoals = 80;
    public int currentCoals = 50;
    private int currency;
    private CarMovement car;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currency = 0;
            maxCarSpeed = carSpeed;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void RefillCoal() {
        CarMovement car = GameObject.Find("Car").GetComponent<CarMovement>();
        car.RefillCoal();
        Debug.Log("Car has been refilled!!!");
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
            // currencyLabel.text = "You earned: " + currencyEarned.ToString() + " Screws";
            // gameOverScreen.SetActive(true);
            SceneManager.LoadScene("Upgrade");
            Debug.Log("Total currency: " + currency.ToString());
        }
    }

    public void RestartGame() {
        gameRunning = true;
        // gameOverScreen.SetActive(false);
        carSpeed = maxCarSpeed;
        currentCoals = maxCoals;
    }

    public int GetCurrency() {
        return currency;
    }
}
