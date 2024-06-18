using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSesiBaslat : MonoBehaviour
{
   [SerializeField] GameObject ButtonSesleri;

   public void SesiOynat()
   {
     Instantiate(ButtonSesleri,transform.position,transform.rotation);
   }
}
