using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private float length, startposX;
    public GameObject cam;
    public float parallaxEffect;

    void Start() {
        startposX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distX = (cam.transform.position.x * parallaxEffect);
        // Distance traveled on X-axis

        transform.position = new Vector3((startposX + distX), transform.position.y, transform.position.z);
        if (temp > startposX + length) startposX += length;
        else if (temp < startposX - length) startposX -= length;
    }
}
