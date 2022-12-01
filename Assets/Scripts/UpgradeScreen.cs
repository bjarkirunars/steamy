using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    public TMPro.TextMeshProUGUI bankLabel;
    private Toggle[] speedToggleList;
    private Toggle[] jumpToggleList;
    public int priceMultiplier;

    public void LoadScene(string scenename)
    {
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(scenename);
    }

    public void IncreaseSpeed(GameObject obj)
    {
        if (
            obj.GetComponent<Toggle>().isOn &&
            int.Parse(obj.name.Substring(obj.name.IndexOf("0")))*1000 > GameManager.instance.maxCarSpeed &&
            GameManager.instance.currency >= int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) * priceMultiplier
            )
        {
            GameManager.instance.maxCarSpeed = int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) * 1000;
            obj.GetComponent<Toggle>().interactable = false;
            GameManager.instance.currency -= int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) * priceMultiplier;
            bankLabel.text = "Total Screws: " + GameManager.instance.currency;
        }
    }

    public void IncreaseJump(GameObject obj)
    {
        if (
            obj.GetComponent<Toggle>().isOn &&
            int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) > GameManager.instance.jumpHeight &&
            GameManager.instance.currency >= int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) * priceMultiplier
            )
        {
            GameManager.instance.jumpHeight = int.Parse(obj.name.Substring(obj.name.IndexOf("0")));
            obj.GetComponent<Toggle>().interactable = false;
            GameManager.instance.currency -= int.Parse(obj.name.Substring(obj.name.IndexOf("0"))) * priceMultiplier;
            bankLabel.text = "Total Screws: " + GameManager.instance.currency;
        }
    }

    void Start()
    {
        priceMultiplier = 30;
        bankLabel.text = "Total Screws: " + GameManager.instance.currency;
        speedToggleList = GameObject.Find("SpeedParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in speedToggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.maxCarSpeed >= togglerNumber * 1000)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
            if (GameManager.instance.currency < togglerNumber * priceMultiplier)
            {
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
            if (GameManager.instance.currency < togglerNumber * priceMultiplier)
            {
                toggler.interactable = false;
            }
        }
    }

    void FixedUpdate()
    {
        speedToggleList = GameObject.Find("SpeedParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in speedToggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.maxCarSpeed >= togglerNumber * 1000)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
            if (GameManager.instance.currency < togglerNumber * priceMultiplier)
            {
                toggler.interactable = false;
            }
        }
        jumpToggleList = GameObject.Find("JumpParent").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggler in jumpToggleList)
        {
            var togglerNumber = int.Parse(toggler.name.Substring(toggler.name.IndexOf("0")));
            if (GameManager.instance.jumpHeight * 5 >= togglerNumber)
            {
                toggler.isOn = true;
                toggler.interactable = false;
            }
            if (GameManager.instance.currency < togglerNumber * priceMultiplier)
            {
                toggler.interactable = false;
            }
        }
    }
}
