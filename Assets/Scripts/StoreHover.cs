using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI priceLabel;
    public GameObject[] toggleList;
    private int priceToPay;

    void FixedUpdate()
    {
        foreach (GameObject toggler in toggleList)
        {
            if (GameManager.instance.carSpeed >= int.Parse(toggler.name.Substring(6)))
            {
                toggler.GetComponent<Toggle>().isOn = true;
                toggler.GetComponent<Toggle>().interactable = false;
            }
        }
    }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        if (eventData.position.y < 538 && eventData.position.y > 508)
        {
            if (eventData.position.x < 369 && eventData.position.x > 343)
            {
                Debug.Log(eventData.position);
                priceToPay = int.Parse(toggleList[0].name.Substring(6));
                toggleList[0].GetComponent<Toggle>().isOn = true;
                priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
            } else if (eventData.position.x < 417 && eventData.position.x > 391)
            {
                priceToPay = int.Parse(toggleList[0].name.Substring(6));
                priceToPay += int.Parse(toggleList[1].name.Substring(6));
                toggleList[0].GetComponent<Toggle>().isOn = true;
                toggleList[1].GetComponent<Toggle>().isOn = true;
                priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
            } else if (eventData.position.x < 465 && eventData.position.x > 439)
            {
                priceToPay = int.Parse(toggleList[0].name.Substring(6));
                priceToPay += int.Parse(toggleList[1].name.Substring(6));
                priceToPay += int.Parse(toggleList[2].name.Substring(6));
                toggleList[0].GetComponent<Toggle>().isOn = true;
                toggleList[1].GetComponent<Toggle>().isOn = true;
                toggleList[2].GetComponent<Toggle>().isOn = true;
                priceLabel.text = "Price: " + priceToPay.ToString() + " Screws";
            }
        }
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        foreach (GameObject toggler in toggleList)
        {
            toggler.GetComponent<Toggle>().isOn = false;
        }
        priceLabel.text = "Price: 0 Screws";
     }
}
