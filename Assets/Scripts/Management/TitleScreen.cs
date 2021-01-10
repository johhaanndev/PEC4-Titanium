using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartFading());
    }

    private IEnumerator StartFading()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("Start");
    }

}
