using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Transform movingPoint;

    public AudioSource collapseEffect;
    public AudioSource ambience;
    
    public void Update()
    {
        if (movingPoint.position.y >= -25)
        {
            ambience.volume = 0.5f;
            ambience.volume = 0.5f;
        }
    }

}
