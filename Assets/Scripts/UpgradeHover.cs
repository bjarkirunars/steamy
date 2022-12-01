using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI priceLabel;
    private int priceToPay;
    public void OnPointerEnter(PointerEventData eventData)
     {
        priceToPay = int.Parse(this.name.Substring(this.name.IndexOf("0"))) * 30;
        priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        priceLabel.text = "Price: 0 Screws";
     }
}
