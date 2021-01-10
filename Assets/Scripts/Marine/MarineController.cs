using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineController : MonoBehaviour
{
    // essential inspector elements
    private Rigidbody2D rb;
    private Animator anim;

    // health parameters
    [Header("HEALTH PARAMETER")]
    public int maxHealth = 100;
    private int currentHealth;

    // player position to target
    [Header("PLAYER TARGET")]
    public Transform playerPosition;

    // detecting parameters
    [Header("DETECTING PARAMETERS")]
    private bool isRight = true;
    private bool isDetected = false;
    public float detectionDistance = 7f;

    [Header("MOVEMENT PARAMENTERS")]
    public float runSpeed;

    // booleans for animations
    private bool canShoot = true;
    private bool isDead = false;

    [Header("FIRING PARAMETERS")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    public GameObject player;

    private enum State { idle, running};
    private State state;

    private int randomStart;

    [Header("PARTICLES SYSTEMS")]
    public ParticleSystem dieParticles;
    public ParticleSystem shootParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        RandomStart();
        Physics2D.IgnoreLayerCollision(11, 18);
    }

    // Update is called once per frame
    void Update()
    {
        // activate actions only if marine and player is alive
        if (!isDead && !player.gameObject.GetComponent<PlayerController>().GetIsDead())
        {
            Actions();
        }
    }

    // method that makes the marine start in random rotation (to not make everyone look at the same point)
    private void RandomStart()
    {
        randomStart = (int)Random.Range(0f, 10f);
        // rotation by euler angles, this way we rotate everything about the prefab
        if (randomStart < 5)
        {
            isRight = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                180f,
                                                transform.eulerAngles.z);
        }
        else
        {
            isRight = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                0f,
                                                transform.eulerAngles.z);
        }
    }

    // main method
    private void Actions()
    {
        // once the marine detects the player, it remains detected until it dies
        if (PlayerInRange())
        {
            isDetected = true;
            TurnAround();
        }

        // when the player is detected
        if (isDetected)
        {
            // if the player is in range, change animation from running to shoot
            if (PlayerInRange())
            {
                anim.SetBool("isRunning", false);
                //shoot
                if (canShoot)
                {
                    anim.SetBool("isShooting", true);
                    shootParticles.Play();

                    // cooldown
                    canShoot = false;
                    StartCoroutine(ShootCoolDown());
                }
            }
            // if the player is away, the marine pursues it
            else
            {
                anim.SetBool("isShooting", false);
                // pursue until it reaches de detection distance (shooting range)
                if (Mathf.Abs(playerPosition.position.y - transform.position.y) <= 3.5)
                {

                    if (isRight)
                    {
                        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                        anim.SetBool("isRunning", true);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                        anim.SetBool("isRunning", true);
                    }
                }
                else
                {
                    anim.SetBool("isRunning", false);
                }
            }
        }
    }

    // method that determine if player is in detection range
    private bool PlayerInRange()
    {
        if (!player.gameObject.GetComponent<PlayerController>().GetIsDead())
        {
            if (Mathf.Abs(playerPosition.position.x - transform.position.x) < detectionDistance && Mathf.Abs(playerPosition.position.y - transform.position.y) <= 3.5)
            {
                return true;
            }
        }
        return false;
    }
    
    // getter
    public bool GetIsDead()
    {
        return isDead;
    }

    // rotate the marine depending on player location
    private void TurnAround()
    {
        if (playerPosition.transform.position.x < transform.position.x)
        {
            isRight = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                180f,
                                                transform.eulerAngles.z);
        }
        else
        {
            isRight = true; 
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                 0f,
                                                 transform.eulerAngles.z);
        }
    }

    // method for taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeflectedBullet"))
        {
            TakeDamage(100);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // death method
    private void Die()
    {
        Vector2 rightForce = new Vector2(200f, 200f);
        Vector2 leftForce = new Vector2(-rightForce.x, rightForce.y);

        anim.SetBool("isShooting", false);
        if (isRight)
        {
            rb.AddForce(leftForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(rightForce, ForceMode2D.Impulse);
        }

        isDead = true;
        anim.SetBool("isDead", true);

        this.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(DestroyGO());
    }

    private void ShootToIdle()
    {
        anim.SetBool("isShooting", false);
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private IEnumerator DestroyGO()
    {
        dieParticles.Play();
        yield return new WaitForSeconds(0.50f);
        Destroy(gameObject);
    }
}
