using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScapeScene : MonoBehaviour
{
    private float length;
    private float startPosX;
    private float startPosY;

    public GameObject cam;
    public float parallaxEffect;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

    }
}
