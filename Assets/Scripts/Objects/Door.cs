using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject player;

    private bool canOpen = false;

    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(17, 8);
    }

    // Update is called once per frame
    void Update()
    {
        // door opens when player collected 7 gems and is near
        if (canOpen && Mathf.Abs(player.transform.position.x - transform.position.x) <= 3f)
        {
            if (transform.position.y < -49f)
            {
                anim.SetTrigger("Move");
                rb.velocity = new Vector2(0f, 1f);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void SetCanOpen(bool canOpen)
    {
        this.canOpen = canOpen;
    }

    public void SoundOn()
    {
        sound.Play();
    }
}
