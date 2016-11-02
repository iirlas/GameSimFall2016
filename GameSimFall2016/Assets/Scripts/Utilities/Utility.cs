using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Utility  {

    public static bool isFading { private set; get; }

    public static IEnumerator fadeScreen (Color start, Color end, float speed, float delay)
    {
        if ( isFading )
        {
            yield return null;
        }
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        Image fadeImage = canvas.gameObject.AddComponent<Image>();

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
