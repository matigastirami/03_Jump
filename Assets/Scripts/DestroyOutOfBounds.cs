using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    private float maxLeftX;

    // Start is called before the first frame update
    void Start()
    {
        maxLeftX = -5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < maxLeftX)
        {
            Destroy(gameObject);
        }
    }
}
