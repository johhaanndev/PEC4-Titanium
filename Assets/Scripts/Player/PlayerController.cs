using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // inspector paramenters
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D coll;
    [SerializeField] private LayerMask ground;

    [Header ("MOVEMENT PARAMETERS")]
    public float runForce;
    public float maxSpeed;
    public float jumpForce;

    private enum State { idle, run, jump, fall, hurt, death};
    private State state;

    private float health = 100f;
    private bool isHit = false;
    private bool isDead = false;

    [Header("OTHER GAMEOBJECTS")]
    public GameObject deathMenu;
    public GameObject door;

    [Header("PARTICLES")]
    private int gemsCollected = 0;
    public GameObject gemParticles;

    [Header("AUDIOS")]
    public AudioSource jumpSound;
    public AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(10, 14);
        Physics2D.IgnoreLayerCollision(10, 18);
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Die();
        }
        if (!isHit && !isDead)
        {
            if (health < 100)
            {
                RecorverHealth();
            }
            Movement();
        }

        SwitchState();
        anim.SetInteger("State", (int)state);
        
        // once the player collected 7 gems, door is able to be openned
        if (gemsCollected == 7)
        {
            door.gameObject.GetComponent<Door>().SetCanOpen(true);
        }
    }

    // Method for the basic movement
    private void Movement()
    {
        // player rotates with euler angles, this way we rotate everything about player
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-runForce, 0f));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                180f,
                                                transform.eulerAngles.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(runForce, 0f));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                0f,
                                                transform.eulerAngles.z);
        }
        
        // as movement is made by forces, we put a maximum speed
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (coll.IsTouchingLayers(ground))
            {
                Jump(jumpForce);
            }
        }   
    }

    // method to jump
    private void Jump(float force)
    {
        jumpSound.Play();
        state = State.jump;
        rb.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if it collides with a marine bullet, it takes damage
        if (collision.gameObject.CompareTag("MarineBullet"))
        {
            TakeDamage(10);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        // if it falls on an enemy, it jumps a little
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) && state == State.fall) {
            Jump(jumpForce / 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collecting triggers a particle system, sound and adds one to the gem counter
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.GetComponent<TitaniumBall>().SetCollected(true);
            Instantiate(gemParticles, collision.gameObject.GetComponent<Transform>());
            coinSound.Play();
            gemsCollected++;
        }
        // same as before, but with a special gem
        if (collision.gameObject.CompareTag("DialogBall"))
        {
            collision.gameObject.GetComponent<BallDialog>().SetCollected(true);
            Instantiate(gemParticles, collision.gameObject.GetComponent<Transform>());
            coinSound.Play();
            gemsCollected++;
        }
        // for the scape level. When player falls and triggers this, player dies
        if (collision.gameObject.CompareTag("DeathLine"))
        {
            isDead = true;
        }
        // for the 1st level, it is after the door is open and jumps to the boss level.
        if (collision.gameObject.CompareTag("GoToNextLevel"))
        {
            SceneManager.LoadScene("LevelBoss");
        }
    }

    // method to take damage
    public void TakeDamage(int dmg)
    {
        isHit = true;
        health -= dmg;
        state = State.hurt;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    // method to progressively recover health over time
    private void RecorverHealth()
    {
        health += 1f * Time.deltaTime;
        if (health >= 100f)
        {
            health = 100f;
        }
    }

    // method to make player die and activate death menu
    private void Die()
    {
        deathMenu.SetActive(true);
    }

    // method to switch state
    private void SwitchState()
    {
        if (isDead)
        {
            state = State.death;
        }
        else if (isHit)
        {
            state = State.hurt;
        }
        else
        {
            // when jump, check y velocity for falling animation
            if (state == State.jump)
            {
                if (rb.velocity.y < 0f)
                {
                    state = State.fall;
                }
            }
            // player can fall off of the platform, fall animation activate
            else if (!coll.IsTouchingLayers(ground) && rb.velocity.y < 0f)
            {
                state = State.fall;
            }
            // when hits the floor, switch to idle animation
            else if (state == State.fall)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    state = State.idle;
                }
            }
            // if velocity x is greater than 0.2, switch to run animation
            else if (Mathf.Abs(rb.velocity.x) > 0.5f)
            {
                state = State.run;
            }
            // when stopped, switch to idle animation
            else if (Mathf.Abs(rb.velocity.x) < 0.5f)
            {
                state = State.idle;
            }
        }
    }

    // Getters ans setters

    public int GetGemsCollected()
    {
        return gemsCollected;
    }

    public void AddGemCollected()
    {
        coinSound.Play();
        gemsCollected++;
    }

    public int GetHealth()
    {
        return (int)health;
    }

    public bool GetHurt()
    {
        return isHit;
    }

    public void SetHurt(bool isHit)
    {
        this.isHit = isHit;
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}