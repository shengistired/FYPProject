using System.Collections;
using UnityEngine;

public class TileDropController : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collider)
   {
       if(collider.gameObject.CompareTag("Player"))
       Destroy(this.gameObject);

        //add to inventory code here


   }
}
