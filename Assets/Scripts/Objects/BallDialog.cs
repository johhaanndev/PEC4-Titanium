using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDialog : MonoBehaviour
{

    private CircleCollider2D cc;
    private bool isCollected = false;

    public GameObject strikerText;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        strikerText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            JustCollected();
        }
    }

    public void JustCollected()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        cc.enabled = false;
        
        StartCoroutine(DestroyBall());
        strikerText.SetActive(true);
    }

    private IEnumerator DestroyBall()
    {
        
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void SetCollected(bool isCollected)
    {
        this.isCollected = isCollected;
    }
}
