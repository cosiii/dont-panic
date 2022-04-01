using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : BasePlayer
{
    public static Player1 Instance;
   [SerializeField] public GameObject highlight;

   void Awake(){
       Instance = this;
   }
}
