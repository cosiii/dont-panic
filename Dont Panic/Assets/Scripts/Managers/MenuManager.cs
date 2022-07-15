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

[SerializeField] public GameObject itemText, TemporaryModals; 
[SerializeField] public GameObject PlayerText, DoorNameText; 
[SerializeField] public GameObject TextForPlayerModal;
[SerializeField] public GameObject NameTextPlayerOne, NameTextPlayerTwo; 

[SerializeField] public GameObject AdditionalInventory1, AdditionalInventory2; 
[SerializeField] public GameObject ExitTextModal, ExitText;


[SerializeField] public Image pl1PantryFeature, pl1DiningFeature, pl1HallwayFeature, pl1SurgeryFeature;
[SerializeField] public Image pl2PantryFeature, pl2DiningFeature, pl2HallwayFeature, pl2SurgeryFeature;

[SerializeField] public bool TemporaryModalsRotated = false;
void Awake(){
    Instance = this;
}

// GAME WON
public void ShowExitText(){
    Debug.Log("exit found");
ExitTextModal.SetActive(true);
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
public void CloseDoorFoundModal(){
   Animator anim = doorModal.GetComponentInChildren<Animator>();
    anim.SetFloat("Speed", 100);
}

public void ShowAdditionalInventory1(){
    AdditionalInventory1.SetActive(true);
}

public void ShowAdditionalInventory2(){
    AdditionalInventory2.SetActive(true);
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
