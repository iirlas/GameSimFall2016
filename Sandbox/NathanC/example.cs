
public class example : MonoBehaviour
{

   public Canvas uiCanvas;
   private BoxCollider myHitbox;
   private Text playerScore;

   //===============================================================
   // Use this for initialization
	void Start () {
        if (myHitbox == null)
        {
            myHitbox = this.GetComponent<BoxCollider>();
        }

	}

   //===============================================================
	// Update is called once per frame
	void Update () {
	    
	}

   //===============================================================
   // When something enters this object's collision box, delete it
   // and the object this collision box belongs to.
   void onCollisionEnter(Collision other)
   {
       Debug.Log("Bullet collided with enemy");
       
       if (other.gameObject.tag == "PlayerBullet")
       {
           int score = int.Parse(uiCanvas.GetComponent<Text>().text);
           score++;
           uiCanvas.GetComponent<Text>().text = score.ToString();

           other.gameObject.GetComponent<HorizMovement>().enabled = false;

           Destroy(other.gameObject);
           Destroy(this.gameObject);
       }
   }
}
