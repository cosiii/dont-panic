using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    //public GameObject patrol, floorTileHighlight;

    public void Awake(){
        Instance = this;
    }

    public void Update(){
        //Animator animation = patrol.GetComponent<Animator>();
          //animation.SetTrigger("PatrolCollision");

    //Animator animation = floorTileHighlight.GetComponent<Animator>();
         // animation.SetTrigger("blinkTiles");
    }


    
}
