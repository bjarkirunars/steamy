using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwController : MonoBehaviour
{
    // Start is called before the first frame update
    public int penalityWeight;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.carSpeed -= penalityWeight;
        Destroy(collision.gameObject);
    }
}
