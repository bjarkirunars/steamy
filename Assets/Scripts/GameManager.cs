using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int carSpeed = 1;
    public int maxCoalAmount;
    public int currentCoals = 50;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RefillCoal() {
        CarMovement car = GameObject.Find("Car").GetComponent<CarMovement>();
        car.RefillCoal();
        Debug.Log("Car has been refilled!!!");
    }

    private void FixedUpdate()
    {
        if (currentCoals <= 0)
        {
            carSpeed = 0;
        }
    }
}
