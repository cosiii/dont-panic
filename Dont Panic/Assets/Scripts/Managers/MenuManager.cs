using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;

   [SerializeField] public GameObject doorModal, doorFoundModal, doorFoundModalHeading, doorFoundModalText; 

   [SerializeField] public Image doorFoundModalImage;

   [SerializeField] public GameObject  itemModal, itemText, inventoryIsFullText, itemImage, TemporaryModals; 

   //[SerializeField] public GameObject helpers; 

   [SerializeField] public GameObject PlayerText, DoorNameText; 
    [SerializeField] public GameObject TextForPlayerModal;
    [SerializeField] public GameObject NameTextPlayerOne, NameTextPlayerTwo; 

    [SerializeField] public GameObject AdditionalInventory1, AdditionalInventory2; 
    [SerializeField] public GameObject GameWonModal;

    [SerializeField] public Image pl1PantryFeature, pl1DiningFeature, pl1HallwayFeature, pl1SurgeryFeature;
    [SerializeField] public Image pl2PantryFeature, pl2DiningFeature, pl2HallwayFeature, pl2SurgeryFeature;

   public bool TemporaryModalsRotated = false;
void Awake(){
    Instance = this;
   // NameTextPlayerOne.GetComponentInChildren<TextMeshProUGUI>().text= Buttons.Instance.nameText1.text;
    //NameTextPlayerTwo.GetComponentInChildren<TextMeshProUGUI>().text= Buttons.Instance.nameText2.text;
}

// GAME WON
public void ShowGameWonModal(){
GameWonModal.SetActive(true);
}

public void HideHelpers(){
//helpers.SetActive(false);
}
// DOOR MODAL
public void ShowDoorModal(){
    doorModal.SetActive(true);
}

public void UpdateDoorFoundModal(string heading, string text){
    doorFoundModalHeading.GetComponentInChildren<Text>().text = heading;
    doorFoundModalText.GetComponentInChildren<Text>().text = text;
}

// VIA BUTTON
public void CloseDoorFoundModal(string heading, string text){
   doorFoundModal.SetActive(false);
}

public void ShowAdditionalInventory1(){
    AdditionalInventory1.SetActive(true);
}

public void ShowAdditionalInventory2(){
    AdditionalInventory2.SetActive(true);
}

// ITEM MODAL

public void AnimateItemModal(){
    Animator animation = itemModal.GetComponent<Animator>();
    animation.SetTrigger("ItemCollision");
}

public void AnimateDoorModal(string triggerName){
    Animator animation = doorModal.GetComponent<Animator>();
    animation.SetTrigger(triggerName);
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

public void ShowPlayersTurn(){
    if( GameManager.Instance.GameState == GameState.Player1Turn){
        Player1.Instance.highlight.SetActive(true);
    } else if( GameManager.Instance.GameState == GameState.Player2Turn){
        Player2.Instance.highlight.SetActive(true);
    }
}

}
