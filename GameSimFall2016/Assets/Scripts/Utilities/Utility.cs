using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Utility  {

    public static bool isFading { private set; get; }
    //------------------------------------------------------------------------------------------------
    //Nathan was not here
    public static IEnumerator fadeScreen (Color start, Color end, float speed, float delay)
    {
        if ( isFading )
        {
            yield return null;
        }
        GameObject gameObject = new GameObject("FadeObject");
        Canvas canvas = gameObject.AddComponent<Canvas>();
        Image fadeImage = gameObject.AddComponent<Image>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;


        isFading = true;

        fadeImage.color = start;
        
        bool doneFading = false;
        while (!doneFading)
        {
            yield return new WaitForSeconds(delay);

            doneFading = (fadeImage.color.isCloseTo(end, 0.01f));
            if (doneFading)
                continue;

            fadeImage.color = Color.Lerp(fadeImage.color, end, speed);
            //Debug.Log(fadeImage.color + " " + end);
        }

        //GameObject.Destroy(fadeImage);
        isFading = false;
        yield return null;
    }
}
