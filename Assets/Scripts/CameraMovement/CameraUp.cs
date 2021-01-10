using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUp : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;

    public float upSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, upSpeed);
    }
}
