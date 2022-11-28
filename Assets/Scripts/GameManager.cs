using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int carSpeed;
    private int maxCoals = 50;
    public int currentCoals = 50;
    // Start is called before the first frame update
    void Awake()
    {
        currentCoals = maxCoals;
        instance = this;
    }

    private void FixedUpdate()
    {
        if (currentCoals <= 0)
        {
            carSpeed = 0;
        }
    }
}
