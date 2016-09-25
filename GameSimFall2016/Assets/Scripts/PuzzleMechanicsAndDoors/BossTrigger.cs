using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour
{

   // Use this for initialization
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   void OnTriggerEnter(Collider other)
   {
      if (other.name.Equals("Kira"))
      {
         GameObject.FindGameObjectWithTag("Boss").GetComponent<JaguarBoss>().triggerBoss();
      }
   }
}
