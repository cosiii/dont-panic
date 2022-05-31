using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public GameObject patrol;

    public void Awake(){
        Instance = this;
    }

    public void Update(){
        //Animator animation = patrol.GetComponent<Animator>();
          //animation.SetTrigger("PatrolCollision");
    }


    public void AnimateBoom(){
    
}
}
