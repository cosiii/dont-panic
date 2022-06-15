using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public Tile floorTile;

    public GameObject SlotToAnimate = null;

    public void Awake(){
        Instance = this;
    }

    public void AnimateHighlightTiles(){
    Animator highlight = floorTile.highlight.GetComponent<Animator>();
    highlight.SetTrigger("BlinkTiles");
    Debug.Log("should animate highlight tiles");
    }

    public void AnimateInventoryPoint(){
        Animator inv = SlotToAnimate.GetComponentInChildren<Animator>();
        inv.SetTrigger("onCollect");
    }


    
}
