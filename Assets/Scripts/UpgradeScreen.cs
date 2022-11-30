using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
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
        }
    }
}
