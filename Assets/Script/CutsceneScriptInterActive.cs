using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScriptInterActive : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image[] img;
   
    public void OnFadeIn()
    {
        StartCoroutine(FadeImage(true));
    }

    public void OnFadeOut()
    {
        StartCoroutine(FadeImage(false));
    }
 
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (int imageNumber = 0; imageNumber < img.Length; imageNumber++)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    // set color with i as alpha
                    img[imageNumber].color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            
            
        }
        // fade from transparent to opaque
        else
        {

            for (int imageNumber = 0; imageNumber < img.Length; imageNumber++)
            {
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    img[imageNumber].color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            // loop over 1 second

            
        }
    }
}
