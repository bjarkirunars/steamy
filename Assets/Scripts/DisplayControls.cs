using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControls : MonoBehaviour
{
    public GameObject controlsWindow;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            isActive = !isActive;
            controlsWindow.SetActive(isActive);
        }
    }
}
