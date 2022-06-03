using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;

   [SerializeField] public GameObject doorNameObject, doorTextObject, doorModal, doorFoundModal, doorFoundModalHeading, doorFoundModalText; 

   [SerializeField] public GameObject  itemModal, itemText, inventoryIsFullText, itemImage, TemporaryModals; 

   [SerializeField] public GameObject helpers; 

   [SerializeField] public GameObject TextForPlayerModal, PlayerText; 
    [SerializeField] public GameObject NameTextPlayerOne, NameTextPlayerTwo; 

    [SerializeField] public GameObject AdditionalInventory1, AdditionalInventory2; 
    [SerializeField] public GameObject GameWonModal;


   public bool TemporaryModalsRotated = false;
void Awake(){
    Instance = this;
    //NameTextPlayerOne.GetComponentInChildren<TextMeshProUGUI>().text= Buttons.Instance.nameText1.text;
    //NameTextPlayerTwo.GetComponentInChildren<TextMeshProUGUI>().text= Buttons.Instance.nameText2.text;
}

// GAME WON
public void ShowGameWonModal(){
GameWonModal.SetActive(true);
}
// DOOR MODAL
public void ShowDoorModal(){
    doorModal.SetActive(true);
}

public void ShowDoorFoundModal(string heading, string text){
    doorFoundModal.SetActive(true);
    doorFoundModalHeading.GetComponentInChildren<Text>().text = heading;
    doorFoundModalText.GetComponentInChildren<Text>().text = text;
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
    itemText.SetActive(true);
    inventoryIsFullText.SetActive(false);
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
    itemText.SetActive(false);
    itemImage.SetActive(false);
}

public void ShowPlayersTurn(){
    if( GameManager.Instance.GameState == GameState.Player1Turn){
        Player1.Instance.highlight.SetActive(true);
    } else if( GameManager.Instance.GameState == GameState.Player2Turn){
        Player2.Instance.highlight.SetActive(true);
    }
}

}
