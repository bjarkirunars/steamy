using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelToGroundCheckR : MonoBehaviour
{
    private bool isTouching;
    public  bool groundCheckR(float groundCheckRadius, int groundLayer)
    {
        isTouching = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        return isTouching;
    }
}
