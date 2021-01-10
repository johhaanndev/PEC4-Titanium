using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoninDialog : MonoBehaviour
{
    [Header("TEXT PARAMETERS")]
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    [Header("OTHERS")]
    public GameObject player;
    public GameObject gemParticles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }


    private IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(1f);
        NextSentence();
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            if (index == sentences.Length - 2)
            {
                player.GetComponent<PlayerController>().AddGemCollected();
                Instantiate(gemParticles, player.transform);
            }
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
}
