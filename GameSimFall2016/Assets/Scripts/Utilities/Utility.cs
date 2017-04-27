using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Contains various general purpose functions.
public class Utility  {
    // determines whether the Fade is taking place.
    public static bool isFading { private set; get; }
	public static GameObject fadeObject;
    //------------------------------------------------------------------------------------------------
    // Fades the Screen over a set amount of time.
    // Used with StartCoroutine() for threaded used.
    public static IEnumerator fadeScreen (Color start, Color end, float speed, float delay)
    {
        if ( isFading )
        {
            yield return null;
        }
		Canvas canvas;
		Image fadeImage;
		if (fadeObject == null)
		{
			fadeObject = new GameObject ("FadeObject");
			canvas = fadeObject.AddComponent<Canvas> ();
			fadeImage = fadeObject.AddComponent<Image> ();
		}
		else
		{
			canvas = fadeObject.GetComponent<Canvas> ();
			fadeImage = fadeObject.GetComponent<Image> ();	
		}
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
