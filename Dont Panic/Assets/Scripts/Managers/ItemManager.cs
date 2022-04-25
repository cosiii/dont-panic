using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;
    public Image oldImage;
    public Sprite Item1, Item2, Item3, Item4, Item5, Item6, Item7;
  
  
   void Awake(){
        Instance = this;
    }

    public void ChangeModalImage(){

    Debug.Log("item picked up by " + MenuManager.Instance.selectedPlayerObject.GetComponentInChildren<Text>().text + "    " + InventoryManager.Instance.lastDestroyedItem);

    if(InventoryManager.Instance.lastDestroyedItem == "Item1"){
        oldImage.sprite = Item1;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item2"){
        oldImage.sprite = Item2;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item3"){
        oldImage.sprite = Item3;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item4"){
        oldImage.sprite = Item4;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item5"){
        oldImage.sprite = Item5;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item6"){
        oldImage.sprite = Item6;
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item7"){
        oldImage.sprite = Item7;
    }
    
}
}
