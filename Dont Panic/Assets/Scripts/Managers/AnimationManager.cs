using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public FloorTile floorTile;

    public void Awake(){
        Instance = this;
    }

    public void Start(){
        Instance = this;
    }

    public void AnimateHighlightTiles(){
    Animator highlight = floorTile.highlight.GetComponent<Animator>();
    highlight.SetBool("blinkTiles", true);
    }


    
}
