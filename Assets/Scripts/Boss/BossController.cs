using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("MAIN ELEMENTS")]
    public Transform playerPosition;
    public Transform bossPosition;
    public BossController boss;

    [Header("DO NOT TOUCH")]
    public bool isRight = false;

    [Header("AUDIOS")]
    public AudioSource hammerThrowSound;
    public AudioSource hammerHitSound;

    public void TurnAround()
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

    // this method was created to fix the position after leap attack animation
    public void MoveToPoint()
    {
        if (boss.isRight)
        {
            bossPosition.position = new Vector2(bossPosition.position.x + 4.534f, bossPosition.position.y);
        }
        else
        {
            bossPosition.position = new Vector2(bossPosition.position.x - 4.534f, bossPosition.position.y);
        }
    }

    public void HammerThrowPlay()
    {
        hammerThrowSound.Play();
    }

    public void HammerHitPlay()
    {
        hammerHitSound.Play();
    }
}
