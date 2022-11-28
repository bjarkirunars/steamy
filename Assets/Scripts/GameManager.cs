using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int carSpeed;
    public int maxCoalAmount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void RefillCoal() {
        CarMovement car = GameObject.Find("Car").GetComponent<CarMovement>();
        car.RefillCoal();
        Debug.Log("Car has been refilled!!!");
    }
}
