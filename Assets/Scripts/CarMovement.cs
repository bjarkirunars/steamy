using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    public int speed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (Input.GetAxis("Horizontal") <0)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
        }
    }
}