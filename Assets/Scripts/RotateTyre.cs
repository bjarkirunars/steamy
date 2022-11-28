using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTyre : MonoBehaviour
{
    public GameObject tyre;
    // Update is called once per frame
    void Update() {
        float yAngle = (90 * Time.deltaTime * GameManager.instance.carSpeed);
        tyre.transform.Rotate(0, yAngle, 0, Space.Self);
    }
}
