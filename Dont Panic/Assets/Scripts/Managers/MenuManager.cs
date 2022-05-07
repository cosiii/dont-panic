using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;
   [SerializeField] public GameObject selectedPlayerObject, tileObject, tileUnitObject; 

   [SerializeField] public GameObject Item1Object, Item2Object, doorNameObject, doorTextObject; 

   [SerializeField] public GameObject doorModal, itemModal, inventoryIsFullText, itemImage; 

   [SerializeField] public GameObject yourTurnSign1, yourTurnSign2; 
   public bool itemModalRotated = false; 
   public bool doorModalRotated = false;
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


// DOOR MODAL
public void ShowDoorModal(){
    doorModal.SetActive(true);
    Item1Object.SetActive(true);
    doorNameObject.SetActive(true);
    doorTextObject.SetActive(true);
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

// ITEM MODAL
public void HideItemModal(){
    itemModal.SetActive(false);
}

public void ShowItemModal(){
    itemModal.SetActive(true);
    itemImage.SetActive(true);
}

public void AnimateItemModal(){
    Animator animation = itemModal.GetComponent<Animator>();
    animation.SetTrigger("ItemCollision");
}


public void RotateItemModalToPlayer1(){
    if( itemModalRotated == true){
        itemModal.transform.Rotate(180, 180, 0);
    } 
    itemModalRotated = false;
}

public void RotateItemModalToPlayer2(){
    // wenns schon rotiert is dann net
    if( itemModalRotated == false){
        itemModal.transform.Rotate(180, 180, 0);
    } 
    itemModalRotated = true;
}

public void ShowInventoryIsFullText(){
    itemModal.SetActive(true);
    inventoryIsFullText.SetActive(true);
    itemImage.SetActive(false);
}

public void ShowPlayersTurn(){
    if( GameManager.Instance.GameState == GameState.Player1Turn){
        yourTurnSign1.SetActive(true);
        yourTurnSign2.SetActive(false);
    } else if( GameManager.Instance.GameState == GameState.Player2Turn){
        yourTurnSign1.SetActive(false);
        yourTurnSign2.SetActive(true);
    }
}

public void RotateDoorModalToPlayer1(){
    if( doorModalRotated == true){
        doorModal.transform.Rotate(180, 180, 0);
    } 
    doorModalRotated = false;
}

public void RotateDoorModalToPlayer2(){
    if( doorModalRotated == false){
        doorModal.transform.Rotate(180, 180, 0);
    } 
    doorModalRotated = true;
}

}
