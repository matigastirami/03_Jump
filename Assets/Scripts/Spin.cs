using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private float translateSpeed = 9f;
    private float rotateSpeed = 60;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.left * translateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
