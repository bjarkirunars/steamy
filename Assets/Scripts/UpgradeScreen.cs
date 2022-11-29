using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    public void LoadScene(string scenename)
    { 
        SceneManager.LoadScene(scenename);
    }

    public void IncreaseSpeed(GameObject obj)
    {
        if (obj.GetComponent<Toggle>().isOn)
        {
            GameManager.instance.carSpeed += int.Parse(obj.name.Substring(6));
        } else
        {
            GameManager.instance.carSpeed -= int.Parse(obj.name.Substring(6));
        }
    }
}
