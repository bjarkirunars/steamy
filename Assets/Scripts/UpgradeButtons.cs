using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UpgradeButtons : MonoBehaviour
{
    public TMPro.TextMeshProUGUI bankLabel;
    public TMPro.TextMeshProUGUI speedLabel;
    public TMPro.TextMeshProUGUI jumpLabel;
    public TMPro.TextMeshProUGUI nitroLabel;
    public TMPro.TextMeshProUGUI coalLabel;
    public TMPro.TextMeshProUGUI explanationLabel;
    public TMPro.TextMeshProUGUI priceLabel;
    public float speedMultiplier = 1.1f;
    public float jumpMultiplier = 1.3f;
    public float coalMultiplier = 1.3f;
    private int priceToPay;
    private int priceLabelPrice;

    void Awake()
    {
        UpdateValues();
    }

    public void LoadScene(string scenename)
    {
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(scenename);
    }

    void UpdateValues()
    {
        bankLabel.text = "Total Screws: " + GameManager.instance.currency.ToString();
        speedLabel.text = "Speed: " + GameManager.instance.maxCarSpeed.ToString();
        jumpLabel.text = "Jumps: " + GameManager.instance.jumpHeight.ToString() + "Nm";
        nitroLabel.text = "Nitro: " + GameManager.instance.nitroCharges.ToString() + " Charges";
        coalLabel.text = "Efficiency: " + (Mathf.Round(1/GameManager.instance.coalSpendTime)).ToString() + "/sec";
        priceLabel.text = "Price: " + priceLabelPrice.ToString() + " Screws";
    }

    public void upgradeSpeed()
    {
        priceToPay = GameManager.instance.speedLevel * 50;
        if (GameManager.instance.currency >= priceToPay)
        {
            GameManager.instance.maxCarSpeed = (int)(GameManager.instance.maxCarSpeed * speedMultiplier);
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.speedLevel += 1;
            priceLabelPrice = GameManager.instance.speedLevel * 50;
            UpdateValues();
        }
    }

    public void upgradeJump()
    {
        priceToPay = GameManager.instance.jumpLevel * 300;
        if (GameManager.instance.currency > priceToPay)
        {
            if (GameManager.instance.jumpHeight == 0)
            {
                GameManager.instance.jumpHeight = 15;
            } else {
                GameManager.instance.jumpHeight = (int)(GameManager.instance.jumpHeight * jumpMultiplier);
            }
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.jumpLevel += 1;
            priceLabelPrice = GameManager.instance.jumpLevel * 300;
            UpdateValues();
        }
        
    }

    public void upgradeCoals()
    {
        priceToPay = GameManager.instance.coalUpgradeLevel * 100;
        if (GameManager.instance.currency >= priceToPay)
        {
            GameManager.instance.coalSpendTime = GameManager.instance.coalSpendTime + 0.05f;
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.coalUpgradeLevel += 1;
            priceLabelPrice = GameManager.instance.coalUpgradeLevel * 100;
            UpdateValues();
        }
        
    }

    public void upgradeNitro()
    {
        priceToPay = GameManager.instance.nitroLevel * 500;
        if (GameManager.instance.currency >= priceToPay)
        {
            GameManager.instance.nitroCharges += 1;
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.nitroLevel += 1;
            priceLabelPrice = GameManager.instance.nitroLevel * 500;
            UpdateValues();   
        }
        
    }
}
