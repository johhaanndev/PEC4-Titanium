using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    // Inspector parameters
    private Rigidbody2D rb;
    public GameObject player;
    public AudioSource scapeTheme;

    public Shaker shaker;

    private bool startScape = false;

    // audios
    public AudioSource ambience;
    public AudioSource finishTheme;

    // game objects to instantiate
    public GameObject titaniumText;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject creditsText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        titaniumText.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        creditsText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // follows only the player X position
        rb.transform.position = new Vector2(player.transform.position.x, transform.position.y);
        
        // if scape has started and player is not dead, movingPoint is going up
        if (startScape && !player.GetComponent<PlayerController>().GetIsDead())
        {
            rb.velocity = new Vector2(rb.velocity.x, 2f);
            
        }
        else 
        {
            rb.velocity = new Vector2(0f, 0f);
        }

        // scape secuence ends once it reaches 76m
        if (transform.position.y >= 76f)
        {
            startScape = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // scaping secuence starts when enter de trigger
        if (collision.gameObject.CompareTag("StartScape"))
        {
            StartCoroutine(StartMovingUp());
            scapeTheme.Play();
            shaker.GetComponent<Shaker>().SetStartShake(true);
            StartCoroutine(StopEffects());
        }

        // show final buttons and vicotry
        if (collision.gameObject.CompareTag("WinTrigger"))
        {
            finishTheme.Play();
            StartCoroutine(ShowButtons());
        }
    }

    private IEnumerator StopEffects()
    {
        yield return new WaitForSeconds(5f);
        ambience.Stop();
    }
    private IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(5f);
        titaniumText.SetActive(true);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
        creditsText.SetActive(true);
    }

    private IEnumerator StartMovingUp()
    {
        yield return new WaitForSeconds(8f);
        startScape = true;
    }

    // getter
    public bool GetStart()
    {
        return startScape;
    }
}
