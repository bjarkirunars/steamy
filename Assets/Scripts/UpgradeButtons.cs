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
    public float speedMultiplier = 1.3f;
    public float jumpMultiplier = 1.3f;
    public float coalMultiplier = 1.3f;
    private int priceToPay;

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
    }

    public void upgradeSpeed()
    {
        GameManager.instance.maxCarSpeed = (int)(GameManager.instance.maxCarSpeed * speedMultiplier);
        GameManager.instance.currency -= GameManager.instance.speedLevel * 100;
        GameManager.instance.speedLevel += 1;
        UpdateValues();
    }

    public void upgradeJump()
    {
        if (GameManager.instance.jumpHeight == 0)
        {
            GameManager.instance.jumpHeight = 15;
        } else {
            GameManager.instance.jumpHeight = (int)(GameManager.instance.jumpHeight * jumpMultiplier);
        }
        GameManager.instance.currency -= GameManager.instance.jumpLevel * 300;
        GameManager.instance.jumpLevel += 1;
        UpdateValues();
    }

    public void upgradeCoals()
    {
        GameManager.instance.coalSpendTime = GameManager.instance.coalSpendTime + 0.05f;
        GameManager.instance.currency -= GameManager.instance.coalUpgradeLevel * 100;
        GameManager.instance.coalUpgradeLevel += 1;
        UpdateValues();
    }

    public void upgradeNitro()
    {
        GameManager.instance.nitroCharges += 1;
        GameManager.instance.currency -= GameManager.instance.nitroLevel * 500;
        GameManager.instance.nitroLevel += 1;
        UpdateValues();
    }
}
