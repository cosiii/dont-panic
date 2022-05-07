using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public GameObject ItemModal;

    public void Awake(){
        Instance = this;
    }

    public void Update(){
        Animator animator = ItemModal.GetComponent<Animator>();
        animator.Play("onCollision", 1);
    }
}
