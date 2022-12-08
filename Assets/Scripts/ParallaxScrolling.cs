using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private float length, height, startposX, startposY;
    public GameObject cam;
    public float parallaxEffect;

    void Start() {
        startposX = transform.position.x;
        // startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        // height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        // Variable to track whether background has traveled its length on the X-axis
        float distX = (cam.transform.position.x * parallaxEffect);
        // Distance traveled on X-axis
        // float distY = (cam.transform.position.y * (1 - parallaxEffect) /2);

        transform.position = new Vector3((startposX + distX), transform.position.y, transform.position.z);
        if (temp > startposX + length) startposX += length;
        else if (temp < startposX - length) startposX -= length;
    }
}
