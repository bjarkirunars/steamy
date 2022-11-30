using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkController : MonoBehaviour
{
    // Start is called before the first frame update
    public int penalityWeight;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "junk")
        {
            StartCoroutine(destroyer(collision));

        }
        
    }
    IEnumerator destroyer(Collision2D collision)
    {
        GameManager.instance.carSpeed -= penalityWeight;
        yield return new WaitForSeconds(2);
        Destroy(collision.gameObject);
        Debug.Log("collision detected");
    }
}
