using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPopUps : MonoBehaviour {

    [TextArea]
    public string text;
    
    [Tooltip("This is the image (paper) behind the text. The text box")]
    public Image textBack;
    [Tooltip("This is the the Text item to put the string to")]
    public Text printHere;
    [Tooltip("Should the text backer/text box show?")]
    public bool printTextBack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Kira"))
        {
           printHere.text = text;

           textBack.enabled = printTextBack;
        }
    }

	void OnTriggerExit(Collider other)
	{
		if (other.name.Equals ("Kira"))
		{
			gameObject.SetActive (false);
		}
	}
}
