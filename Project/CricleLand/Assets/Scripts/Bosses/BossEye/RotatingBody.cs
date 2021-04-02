using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBody : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, new Vector3(0, 0, -1), Time.deltaTime * 10);
    }
}
