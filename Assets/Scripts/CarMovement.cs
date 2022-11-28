using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    public float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * GameManager.instance.carSpeed);
        if (Input.GetButton("Horizontal"))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}