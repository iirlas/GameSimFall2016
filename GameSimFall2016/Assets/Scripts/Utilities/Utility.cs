using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Utility  {

    private static bool isFading = false;
    public delegate void OnFadeEnd();

    public static IEnumerator fadeScreen (Color start, Color end, float speed, float delay, OnFadeEnd proc)
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

            fadeImage.color = Color.Lerp(fadeImage.color, end, speed * Time.deltaTime);
            Debug.Log(fadeImage.color + " " + end);
        }

        GameObject.Destroy(fadeImage);
        proc();
        isFading = false;
        yield return null;
    }
}
