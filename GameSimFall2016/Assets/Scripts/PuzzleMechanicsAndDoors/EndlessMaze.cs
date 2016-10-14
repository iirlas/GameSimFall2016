using UnityEngine;
using System.Collections;

public class EndlessMaze : MonoBehaviour
{
   public GameObject warpPos;

   //==========================================================================
   // Use this for initialization
   void Awake()
   {
   }

   //==========================================================================
   // Use this for initialization through other classes
   void Start()
   {
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
   }

   //==========================================================================
   // Update is called once per frame
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.name.Equals("Kira"))
      {
         float ix = 0;

         foreach (Player unit in PlayerManager.getInstance().players)
         {
            ix+= 1.5f;
            unit.gameObject.transform.position = new Vector3(warpPos.transform.position.x + ix,
                                                             warpPos.transform.position.y,
                                                             warpPos.transform.position.z); 
         }
      }
   }
}
