using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;
   [SerializeField] public GameObject selectedPlayerObject; 

   [SerializeField] public GameObject doorNameObject, doorTextObject; 

   [SerializeField] public GameObject doorModal, itemModal, itemText, inventoryIsFullText, itemImage, TemporaryModals; 

   [SerializeField] public GameObject yourTurnSign1, yourTurnSign2; 

   [SerializeField] public GameObject TextForPlayerModal, PlayerText; 

    [SerializeField] public GameObject AdditionalInventory1, AdditionalInventory2; 

   public bool TemporaryModalsRotated = false;
void Awake(){
    Instance = this;
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
}

public void ShowAdditionalInventory1(){
    AdditionalInventory1.SetActive(true);
}

public void ShowAdditionalInventory2(){
    AdditionalInventory2.SetActive(true);
}

public void HideDoorInfo(){
    
    Debug.Log("hidden doormodal");
    doorModal.SetActive(false);
}

// ITEM MODAL
public void ShowItemModal(){
    itemModal.SetActive(true);
    itemImage.SetActive(true);
}

public void AnimateItemModal(){
    Animator animation = itemModal.GetComponent<Animator>();
    animation.SetTrigger("ItemCollision");
}

public void AnimateDoorModal(){
    Animator animation = doorModal.GetComponent<Animator>();
    animation.SetTrigger("DoorCollision");
}

public void AnimatePlayerText(){
    Animator animation = TextForPlayerModal.GetComponent<Animator>();
    animation.SetTrigger("TextNeeded");
}

public void RotateModalsToPlayer1(){
    if( TemporaryModalsRotated == true){
        TemporaryModals.transform.Rotate(180, 180, 0);
    } 
    TemporaryModalsRotated = false;
}

public void RotateModalsToPlayer2(){
    // wenns schon rotiert is dann net
    if( TemporaryModalsRotated == false){
        TemporaryModals.transform.Rotate(180, 180, 0);
    } 
    TemporaryModalsRotated = true;
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

}
