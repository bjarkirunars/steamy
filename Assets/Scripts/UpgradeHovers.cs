using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeHovers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI priceLabel;
    private int priceToPay;
    public UpgradeButtons buttons;


    public void OnPointerEnter(PointerEventData eventData)
     {
        Button myButton = this.GetComponent<Button>();
        switch (this.transform.parent.name)
        {
            case "Speed":
                if (GameManager.instance.speedLevel < 10)
                {
                    priceToPay = GameManager.instance.speedLevel * 100;
                    if (GameManager.instance.speedLevel > 5) 
                    {priceToPay = GameManager.instance.speedLevel * 200;}
                    priceLabel.text = "Level " + GameManager.instance.speedLevel + "/10\nPrice: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Increase the speed of the car!";
                } else {
                    priceLabel.text = "Level 10/10\nThis has reached max level";
                }
                break;
            case "Jump":
                if (GameManager.instance.jumpLevel == 1)
                {
                    priceToPay = GameManager.instance.jumpLevel * 300;
                    priceLabel.text = "Level " + GameManager.instance.jumpLevel + "/7\nPrice: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Jump over the obstacles!";
                } else if (GameManager.instance.jumpLevel < 7)
                {
                    priceToPay = GameManager.instance.jumpLevel * 300;
                    priceLabel.text = "Level " + GameManager.instance.jumpLevel + "/7\nPrice: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Jump even higher!";
                } else {
                    priceLabel.text = "Level 7/7\nThis has reached max level";
                }
                break;
            case "Coals":
                if (GameManager.instance.coalUpgradeLevel < 10)
                {
                    priceToPay = GameManager.instance.coalUpgradeLevel * 100;
                    priceLabel.text = "Level " + GameManager.instance.coalUpgradeLevel + "/10\nPrice: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Increase efficiency so coals last longer!";
                } else {
                    priceLabel.text = "Level 10/10\nThis has reached max level";
                }
                break;
            case "Nitro":
                if (GameManager.instance.nitroLevel < 4)
                {
                    priceToPay = GameManager.instance.nitroLevel * 500;
                    priceLabel.text = "Level " + GameManager.instance.nitroLevel + "/3\nPrice: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Blast on forward with nitro!!";
                } else {
                    priceLabel.text = "Level 3/3\nThis has reached max level";
                }
                break;
            default:
                // code block
                break;
        }
     }

     public void OnPointerExit(PointerEventData eventData)
     {
        priceLabel.text = "";
        buttons.explanationLabel.text = "";
     }
}
