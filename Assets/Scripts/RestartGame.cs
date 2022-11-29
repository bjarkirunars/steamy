using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartScene(string scenename)
    { 
        GameManager.instance.RestartGame();
        SceneManager.LoadScene(scenename);
    }
}
