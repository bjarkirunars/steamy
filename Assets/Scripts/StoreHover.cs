using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI priceLabel;
    private Toggle[] toggleList;
    private int priceToPay;

    void Start()
    {
        toggleList = GameObject.Find("SpeedParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in toggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.maxCarSpeed >= togglerNumber)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
        }
    }

    void FixedUpdate()
    {
        
    }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        priceToPay = int.Parse(this.name.Substring(this.name.IndexOf("0")));
        priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        priceLabel.text = "Price: 0 Screws";
     }
}
