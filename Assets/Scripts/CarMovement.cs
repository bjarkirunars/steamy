using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    private int carSpeed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        carSpeed = GameManager.instance.carSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * carSpeed);
        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}