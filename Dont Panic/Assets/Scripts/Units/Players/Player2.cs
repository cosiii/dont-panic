using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : BasePlayer
{
 
    public static Player2 Instance;
   [SerializeField] public GameObject highlight;

   void Awake(){
       Instance = this;
   }
}
