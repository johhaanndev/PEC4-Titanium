using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineShootingScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
