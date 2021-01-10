﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 20f;
    private bool isDeflected = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(DestroyGO), 2f);
        // marine bullets only interact with enemies and walls, other colliding layers will be ignored
        Physics2D.IgnoreLayerCollision(11, 12);
        Physics2D.IgnoreLayerCollision(11, 11);
        Physics2D.IgnoreLayerCollision(11, 9);
        Physics2D.IgnoreLayerCollision(11, 14);
        Physics2D.IgnoreLayerCollision(11, 20);
    }

    private void Update()
    {
        rb.velocity = rb.transform.right * speed;
        if (isDeflected)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("no-friction") || collision.gameObject.CompareTag("Door"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void DestroyGO()
    {
        Destroy(gameObject);
    }

    public void SetDeflected(bool deflected)
    {
        isDeflected = deflected;
    }

    public bool GetDeflected()
    {
        return isDeflected;
    }

}