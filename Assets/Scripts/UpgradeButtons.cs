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
    private int priceToPay;
    private int priceLabelPrice;
    public AudioClip buySound;

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
        jumpLabel.text = "Jumps: " + (GameManager.instance.jumpHeight*100).ToString() + "N";
        nitroLabel.text = "Nitro: " + GameManager.instance.nitroCharges.ToString() + " Charges";
        coalLabel.text = "Efficiency: " + (Mathf.Round((1/GameManager.instance.coalSpendTime)*100)/100).ToString() + "/sec";
    }

    public void upgradeSpeed()
    {
        priceToPay = GameManager.instance.speedLevel * 100;
        if (GameManager.instance.speedLevel > 5) 
        {priceToPay = GameManager.instance.speedLevel * 200;}
        if (GameManager.instance.currency >= priceToPay && GameManager.instance.speedLevel < 10)
        {
            GameManager.instance.maxCarSpeed = (int)(GameManager.instance.maxCarSpeed * 1.2f);
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.speedLevel += 1;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            priceToPay = GameManager.instance.speedLevel * 100;
            if (GameManager.instance.speedLevel > 5) 
            {priceToPay = GameManager.instance.speedLevel * 200;}
            if (GameManager.instance.speedLevel < 10)
            {
                priceLabel.text = "Level " + GameManager.instance.speedLevel + "/10\nPrice: " + priceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 10/10\nThis has reached max level";
            }
        }
    }

    public void upgradeJump()
    {
        priceToPay = GameManager.instance.jumpLevel * 300;
        if (GameManager.instance.currency >= priceToPay && GameManager.instance.jumpLevel < 7)
        {
            if (GameManager.instance.jumpHeight == 0)
            {
                GameManager.instance.jumpHeight = 15;
            } else {
                GameManager.instance.jumpHeight = (int)(GameManager.instance.jumpHeight * 1.8f);
            }
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.jumpLevel += 1;
            priceLabelPrice = GameManager.instance.jumpLevel * 300;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            priceToPay = GameManager.instance.jumpLevel * 300;
            if (GameManager.instance.jumpLevel < 7)
            {
                priceLabel.text = "Level " + GameManager.instance.jumpLevel + "/7\nPrice: " + priceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 7/7\nThis has reached max level";
            }
        }
        
    }

    public void upgradeCoals()
    {
        priceToPay = GameManager.instance.coalUpgradeLevel * 100;
        if (GameManager.instance.currency >= priceToPay && GameManager.instance.coalUpgradeLevel < 10)
        {
            GameManager.instance.coalSpendTime = GameManager.instance.coalSpendTime + 0.05f;
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.coalUpgradeLevel += 1;
            priceLabelPrice = GameManager.instance.coalUpgradeLevel * 100;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            priceToPay = GameManager.instance.coalUpgradeLevel * 100;
            if (GameManager.instance.coalUpgradeLevel < 10)
            {
                priceLabel.text = "Level " + GameManager.instance.coalUpgradeLevel + "/10\nPrice: " + priceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 10/10\nThis has reached max level";
            }
        }
        
    }

    public void upgradeNitro()
    {
        priceToPay = GameManager.instance.nitroLevel * 500;
        if (GameManager.instance.currency >= priceToPay && GameManager.instance.nitroLevel < 4)
        {
            GameManager.instance.nitroCharges += 1;
            GameManager.instance.currency -= priceToPay;
            GameManager.instance.nitroLevel += 1;
            priceLabelPrice = GameManager.instance.nitroLevel * 500;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            priceToPay = GameManager.instance.nitroLevel * 500;
            if (GameManager.instance.nitroLevel < 4)
            {
                priceLabel.text = "Level " + GameManager.instance.nitroLevel + "/3\nPrice: " + priceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 3/3\nThis has reached max level";
            }
        }
        
    }
}
