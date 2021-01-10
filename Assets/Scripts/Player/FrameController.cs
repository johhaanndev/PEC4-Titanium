using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    public Image hurtFrame;

    // Start is called before the first frame update
    void Start()
    {
        hurtFrame.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        if (GetComponent<PlayerController>().GetHurt())
        {
            hurtFrame.canvasRenderer.SetAlpha(0.7f);
            hurtFrame.CrossFadeAlpha(0.0f, 1f, false);
            GetComponent<PlayerController>().SetHurt(false);
        }
    }

    public void FrameFadeOut()
    {
        hurtFrame.canvasRenderer.SetAlpha(0.7f);
        hurtFrame.CrossFadeAlpha(0.0f, 1f, false);
    }
}
