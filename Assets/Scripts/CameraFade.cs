using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public bool fade = false;
    public float fadeTime = 1;
    public Image curtains;
    public float curtainAlpha = 0;

    private void Awake()
    {
        curtains = GetComponent<Image>();
    }

    private void Update()
    {
        if (fade)
        {
            //Debug.Log("Going Black..");
            curtainAlpha = Mathf.Lerp(curtains.color.a, 1, fadeTime * Time.deltaTime);
            Color c = new Color(curtains.color.r, curtains.color.g, curtains.color.b, curtainAlpha);
            curtains.color = c;
        }
        else if (!fade)
        {
            //Debug.Log("Seeing the Light...");
            curtainAlpha = Mathf.Lerp(curtains.color.a, 0, fadeTime * Time.deltaTime);
            Color c = new Color(curtains.color.r, curtains.color.g, curtains.color.b, curtainAlpha);
            curtains.color = c;
        }

        //Debug.Log("Curtain Alpha: " + curtainAlpha);
    }
}
