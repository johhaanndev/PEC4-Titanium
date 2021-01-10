using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoninManager : MonoBehaviour
{
    public GameObject guard1;
    public GameObject guard2;

    public GameObject roninText;

    // Start is called before the first frame update
    void Start()
    {
        roninText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (guard1.GetComponent<GuardController>().GetIsDead() && guard2.GetComponent<GuardController>().GetIsDead())
        {
            roninText.SetActive(true);
        }
    }
}
