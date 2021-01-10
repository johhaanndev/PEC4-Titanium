using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;

    public Text lifeText;
    public Text gemText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
        UpdateGemCounter();
    }

    private void UpdateLife()
    {
        lifeText.text = playerController.GetHealth().ToString() + "%";
    }

    private void UpdateGemCounter()
    {
        gemText.text = playerController.GetGemsCollected() + "/7";
    }
}
