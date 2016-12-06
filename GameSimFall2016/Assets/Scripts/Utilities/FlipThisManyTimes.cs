using UnityEngine;
using System.Collections;

public class FlipThisManyTimes : MonoBehaviour {

    public GameObject textStuff;
    public GameObject flipping;
    public GameObject button;
    private Animator textAnim;
    private Animator pageFlip;
    
   

	// Use this for initialization
	void Awake () {
        textAnim = textStuff.GetComponent<Animator>();
        pageFlip = flipping.GetComponent<Animator>();
        
       
	}
	
	// Update is called once per frame
	void Update () {
        if (textAnim.GetCurrentAnimatorStateInfo(0).IsName("StaticPage"))
        {
            pageFlip.SetBool("finished", true);
            button.SetActive(true);

        }


       
        
	
	}
    
}
