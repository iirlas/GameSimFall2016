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
         foreach (Player unit in PlayerManager.getInstance().players)
         {
            unit.gameObject.transform.position = warpPos.transform.position;
         }
      }
   }
}
