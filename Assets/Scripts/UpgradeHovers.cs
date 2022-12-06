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
                if (GameManager.instance.speedLevel < 20)
                {
                    priceToPay = GameManager.instance.speedLevel * 50;
                    priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Increase the speed of the car!";
                } else {
                    buttons.explanationLabel.text = "This has reached max level";
                    myButton.GetComponent<Image>().color = Color.red;
                    myButton.interactable = false;
                }
                break;
            case "Jump":
                if (GameManager.instance.jumpLevel == 1)
                {
                    priceToPay = GameManager.instance.jumpLevel * 300;
                    priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Jump over the obstacles!";
                } else if (GameManager.instance.jumpLevel < 7)
                {
                    priceToPay = GameManager.instance.jumpLevel * 300;
                    priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Jump even higher!";
                } else {
                    buttons.explanationLabel.text = "This has reached max level";
                    myButton.GetComponent<Image>().color = Color.red;
                    myButton.interactable = false;
                }
                break;
            case "Coals":
                if (GameManager.instance.coalUpgradeLevel < 10)
                {
                    priceToPay = GameManager.instance.coalUpgradeLevel * 100;
                    priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Increase efficiency so coals last longer!";
                } else {
                    buttons.explanationLabel.text = "This has reached max level";
                    myButton.GetComponent<Image>().color = Color.red;
                    myButton.interactable = false;
                }
                break;
            case "Nitro":
                if (GameManager.instance.nitroLevel < 4)
                {
                    priceToPay = GameManager.instance.nitroLevel * 500;
                    priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
                    buttons.explanationLabel.text = "Blast on forward with nitro!!";
                } else {
                    buttons.explanationLabel.text = "This has reached max level";
                    myButton.GetComponent<Image>().color = Color.red;
                    myButton.interactable = false;
                }
                break;
            default:
                // code block
                break;
        }
        if (GameManager.instance.currency < priceToPay)
        {
            myButton.GetComponent<Image>().color = Color.red;
            myButton.interactable = false;
        }

     }

     public void OnPointerExit(PointerEventData eventData)
     {
        Button myButton = this.GetComponent<Button>();
        myButton.GetComponent<Image>().color = Color.white;
        myButton.interactable = true;
        priceLabel.text = "Price: 0 Screws";
        buttons.explanationLabel.text = "";
     }
}
