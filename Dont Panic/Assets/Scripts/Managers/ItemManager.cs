using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance;

    public BaseUnit itemUnderneath;

    public bool itemUnderneathFound;
    public Image oldImage, oldImageDoorLeft, oldImageDoorRight;
    public Sprite Item1, Item2, Item3, Item4, Item5, Item6, Item7, noImage;
  
  
   void Awake(){
        Instance = this;
    }

    public void ChangeModal(){
    if(InventoryManager.Instance.lastDestroyedItem == "Item1"){
        oldImage.sprite = Item1;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "suicide note";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item2"){
        oldImage.sprite = Item2;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "bubbles";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item3"){
        oldImage.sprite = Item3;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "syringe";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item4"){
        oldImage.sprite = Item4;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "teddy bear";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item5"){
        oldImage.sprite = Item5;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "leather belt";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item6"){
        oldImage.sprite = Item6;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "guard ID";
    } else if(InventoryManager.Instance.lastDestroyedItem == "Item7"){
        oldImage.sprite = Item7;
        MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "tooth";
    }
}

public void ChangeDoorItemImageLeft(string keyItem){
    if(keyItem == "Item1" ){
        oldImageDoorLeft.sprite = Item1;
    } else if(keyItem == "Item2"){
        oldImageDoorLeft.sprite = Item2;
    } else if(keyItem == "Item3"){
        oldImageDoorLeft.sprite = Item3;
    } else if(keyItem == "Item4"){
        oldImageDoorLeft.sprite = Item4;
    } else if(keyItem == "Item5"){
        oldImageDoorLeft.sprite = Item5;
    } else if(keyItem == "Item6"){
        oldImageDoorLeft.sprite = Item6;
    } else if(keyItem == "Item7"){
        oldImageDoorLeft.sprite = Item7;
    }
}

public void ChangeDoorItemImageRight(string keyItem){
    if(keyItem == "Item1" ){
        oldImageDoorRight.sprite = Item1;
    } else if(keyItem == "Item2"){
        oldImageDoorRight.sprite = Item2;
    } else if(keyItem == "Item3"){
        oldImageDoorRight.sprite = Item3;
    } else if(keyItem == "Item4"){
        oldImageDoorRight.sprite = Item4;
    } else if(keyItem == "Item5"){
        oldImageDoorRight.sprite = Item5;
    } else if(keyItem == "Item6"){
        oldImageDoorRight.sprite = Item6;
    } else if(keyItem == "Item7"){
        oldImageDoorRight.sprite = Item7;
    } else if(keyItem == "none"){
        oldImageDoorRight.sprite = noImage;
    }
}
}
