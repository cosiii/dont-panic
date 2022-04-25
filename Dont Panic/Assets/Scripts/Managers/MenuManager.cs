using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;
   [SerializeField] public GameObject selectedPlayerObject, tileObject, tileUnitObject; 

   [SerializeField] public GameObject Item1Object, Item2Object, doorNameObject; 
   [SerializeField] public GameObject doorModal, itemModal, inventoryIsFullText, itemImage; 
void Awake(){
    Instance = this;
}

public void ShowTileInfo(Tile tile){
    if(tile == null){
        tileObject.SetActive(false);
        tileUnitObject.SetActive(false);
        return;
    }

    tileObject.GetComponentInChildren<Text>().text = tile.TileName;
    tileObject.SetActive(true);
    // if tile has an pccupied unit
    if(tile.OccupiedUnit){
        tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
        tileUnitObject.SetActive(true);
    }
}
public void ShowSelectedPlayer(BasePlayer player){
    if(player == null){
        selectedPlayerObject.SetActive(false);
        return;
    }
    
selectedPlayerObject.GetComponentInChildren<Text>().text = player.UnitName;
selectedPlayerObject.SetActive(true);
}

public void ShowDoorModal(){
    doorModal.SetActive(true);
    Item1Object.SetActive(true);
    doorNameObject.SetActive(true);
    // Item2Object.SetActive(true);
}

public void ShowSecondItem(){
    Item2Object.SetActive(true);
}

public void HideSecondItem(){
    Item2Object.SetActive(false);
}

public void HideDoorInfo(){
    doorModal.SetActive(false);
    // Item2Object.SetActive(true);
}

public void HideItemModal(){
    itemModal.SetActive(false);
}

public void ShowItemModal(){
    itemModal.SetActive(true);
    itemImage.SetActive(true);
}
public void ShowInventoryIsFullText(){
    itemModal.SetActive(true);
    inventoryIsFullText.SetActive(true);
    itemImage.SetActive(false);
}
}
