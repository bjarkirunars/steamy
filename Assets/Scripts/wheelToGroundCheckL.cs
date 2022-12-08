using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelToGroundCheckL : MonoBehaviour
{
    private bool isTouching;

    public bool groundCheckL(float groundCheckRadius, int groundLayer)
    {
        Debug.Log("Car detected");

        isTouching = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        return isTouching;
    }
}
