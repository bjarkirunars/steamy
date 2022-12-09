using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void LoadScene(string scenename)
    {
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(scenename);
    }

    public void ResetGame()
    {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("StartMenu");
    }
}
