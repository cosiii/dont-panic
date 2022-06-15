using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public Tile floorTile;

    public void Awake(){
        Instance = this;
    }

    public void AnimateHighlightTiles(){
    //Animator highlight = floorTile.highlight.GetComponent<Animator>();
    //highlight.SetTrigger("blinkTiles");
    Debug.Log("should animate highlight tiles");
    }


    
}
