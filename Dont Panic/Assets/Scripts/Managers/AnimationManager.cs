using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    public static AnimationManager Instance;

    public GameObject SlotToAnimate;

    public void Awake(){
        Instance = this;
    }

    public void AnimateHighlightTiles(){

        for (int x = 0; x < GridManager.Instance._width; x++)
        {
            for (int y = 0; y < GridManager.Instance._height; y++)
            {
                if( GridManager.Instance.tiles[new Vector2(x,y)].highlight.activeSelf == true){
                Animator highlight = GridManager.Instance.tiles[new Vector2(x,y)].highlight.GetComponent<Animator>();
                highlight.SetTrigger("BlinkTiles");

                };
            }
        }
    }

    public void AnimateInventoryPoint(){
        Animator inv = SlotToAnimate.GetComponentInChildren<Animator>();
        inv.SetTrigger("onCollect");
    }


    
}
