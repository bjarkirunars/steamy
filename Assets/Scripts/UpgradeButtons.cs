using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    public TMPro.TextMeshProUGUI bankLabel;
    public TMPro.TextMeshProUGUI speedLabel;
    public TMPro.TextMeshProUGUI jumpLabel;
    public TMPro.TextMeshProUGUI nitroLabel;
    public TMPro.TextMeshProUGUI coalLabel;
    public TMPro.TextMeshProUGUI explanationLabel;
    public TMPro.TextMeshProUGUI priceLabel;
    private Button speedButton;
    private Button jumpButton;
    private Button coalsButton;
    private Button nitroButton;
    private int speedPriceToPay;
    private int coalPriceToPay;
    private int jumpPriceToPay;
    private int nitroPriceToPay;
    private int priceToPay;
    private int priceLabelPrice;
    public AudioClip buySound;

    void Awake()
    {
        speedButton = GameObject.Find("SpeedButton").GetComponent<Button>();
        jumpButton = GameObject.Find("JumpButton").GetComponent<Button>();
        coalsButton = GameObject.Find("CoalsButton").GetComponent<Button>();
        nitroButton = GameObject.Find("NitroButton").GetComponent<Button>();
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
        
        // Update buttons for speed
        speedPriceToPay = GameManager.instance.speedLevel * 100;
        if (GameManager.instance.speedLevel > 5) 
        {speedPriceToPay = GameManager.instance.speedLevel * 200;}
        if (GameManager.instance.currency < speedPriceToPay || GameManager.instance.speedLevel >= 10)
        {
            speedButton.GetComponent<Image>().color = Color.red;
            speedButton.interactable = false;
        } else {
            speedButton.GetComponent<Image>().color = Color.green;
            speedButton.interactable = true;
        }
        
        // Update buttons for coals
        coalPriceToPay = GameManager.instance.coalUpgradeLevel * 100;
        if (GameManager.instance.currency < coalPriceToPay || GameManager.instance.coalUpgradeLevel >= 10)
        {
            coalsButton.GetComponent<Image>().color = Color.red;
            coalsButton.interactable = false;
        } else {
            coalsButton.GetComponent<Image>().color = Color.green;
            coalsButton.interactable = true;
        }

        // Update buttons for jump
        jumpPriceToPay = GameManager.instance.jumpLevel * 300;
        if (GameManager.instance.currency < jumpPriceToPay || GameManager.instance.jumpLevel >= 7)
        {
            jumpButton.GetComponent<Image>().color = Color.red;
            jumpButton.interactable = false;
        } else {
            jumpButton.GetComponent<Image>().color = Color.green;
            jumpButton.interactable = true;
        }

        // Update buttons for nitro
        nitroPriceToPay = GameManager.instance.nitroLevel * 500;
        if (GameManager.instance.currency < nitroPriceToPay || GameManager.instance.nitroLevel >= 4)
        {
            nitroButton.GetComponent<Image>().color = Color.red;
            nitroButton.interactable = false;
        } else {
            nitroButton.GetComponent<Image>().color = Color.green;
            nitroButton.interactable = true;
        }
    }

    public void upgradeSpeed()
    {
        if (GameManager.instance.currency >= speedPriceToPay && GameManager.instance.speedLevel < 10)
        {
            GameManager.instance.maxCarSpeed = (int)(GameManager.instance.maxCarSpeed * 1.2f);
            GameManager.instance.currency -= speedPriceToPay;
            GameManager.instance.speedLevel += 1;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            if (GameManager.instance.speedLevel < 10)
            {
                priceLabel.text = "Level " + GameManager.instance.speedLevel + "/10\nPrice: " + speedPriceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 10/10\nThis has reached max level";
            }
        }
    }

    public void upgradeJump()
    {
        if (GameManager.instance.currency >= jumpPriceToPay && GameManager.instance.jumpLevel < 7)
        {
            if (GameManager.instance.jumpHeight == 0)
            {
                GameManager.instance.jumpHeight = 15;
            } else {
                GameManager.instance.jumpHeight = (int)(GameManager.instance.jumpHeight * 1.8f);
            }
            GameManager.instance.currency -= jumpPriceToPay;
            GameManager.instance.jumpLevel += 1;
            priceLabelPrice = GameManager.instance.jumpLevel * 300;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            if (GameManager.instance.jumpLevel < 7)
            {
                priceLabel.text = "Level " + GameManager.instance.jumpLevel + "/7\nPrice: " + jumpPriceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 7/7\nThis has reached max level";
            }
        }
        
    }

    public void upgradeCoals()
    {
        if (GameManager.instance.currency >= coalPriceToPay && GameManager.instance.coalUpgradeLevel < 10)
        {
            GameManager.instance.coalSpendTime = GameManager.instance.coalSpendTime + 0.05f;
            GameManager.instance.currency -= coalPriceToPay;
            GameManager.instance.coalUpgradeLevel += 1;
            priceLabelPrice = GameManager.instance.coalUpgradeLevel * 100;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            if (GameManager.instance.coalUpgradeLevel < 10)
            {
                priceLabel.text = "Level " + GameManager.instance.coalUpgradeLevel + "/10\nPrice: " + coalPriceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 10/10\nThis has reached max level";
            }
        }
        
    }

    public void upgradeNitro()
    {
        if (GameManager.instance.currency >= nitroPriceToPay && GameManager.instance.nitroLevel < 4)
        {
            GameManager.instance.nitroCharges += 1;
            GameManager.instance.currency -= nitroPriceToPay;
            GameManager.instance.nitroLevel += 1;
            priceLabelPrice = GameManager.instance.nitroLevel * 500;
            GameManager.instance.PlayClip(buySound);
            UpdateValues();
            if (GameManager.instance.nitroLevel < 4)
            {
                priceLabel.text = "Level " + GameManager.instance.nitroLevel + "/3\nPrice: " + nitroPriceToPay.ToString() + " Screws";
            } else
            {
                priceLabel.text = "Level 3/3\nThis has reached max level";
            }
        }
        
    }
}
