using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI bankLabel;
    public TMPro.TextMeshProUGUI priceLabel;
    private Toggle[] speedToggleList;
    private Toggle[] jumpToggleList;
    private int priceToPay;

    public void LoadScene(string scenename)
    {
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(scenename);
    }

    public void IncreaseSpeed(GameObject obj)
    {
        if (
            obj.GetComponent<Toggle>().isOn &&
            int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) > GameManager.instance.maxCarSpeed
            )
        {
            GameManager.instance.maxCarSpeed = int.Parse(obj.name.Substring(obj.name.IndexOf("0")));
            obj.GetComponent<Toggle>().interactable = false;
        }
    }

    public void IncreaseJump(GameObject obj)
    {
        if (
            obj.GetComponent<Toggle>().isOn &&
            int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) > GameManager.instance.jumpHeight
            )
        {
            GameManager.instance.jumpHeight = int.Parse(obj.name.Substring(obj.name.IndexOf("0")));
            obj.GetComponent<Toggle>().interactable = false;
        }
    }

    void Start()
    {
        bankLabel.text = "Total Screws: " + GameManager.instance.currency;
        speedToggleList = GameObject.Find("SpeedParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in speedToggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.maxCarSpeed >= togglerNumber)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
        }
        jumpToggleList = GameObject.Find("JumpParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in jumpToggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.jumpHeight >= togglerNumber)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
        }
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
