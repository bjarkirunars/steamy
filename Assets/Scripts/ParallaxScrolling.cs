using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start() {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        // Variable to track whether background has traveled its length on the X-axis
        float distX = (cam.transform.position.x * parallaxEffect);
        // Distance traveled on X-axis

        transform.position = new Vector3(startpos + distX, transform.position.y, transform.position.z);
        Debug.Log("Temp: " + temp + " Startpos: " + startpos);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
