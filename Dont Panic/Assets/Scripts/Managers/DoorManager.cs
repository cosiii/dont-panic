using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
  public static DoorManager Instance;
  public string lastVisitedDoor;

  public string[] keyItems1, keyItems2, keyItems3, keyItems4, keyItems5;

  public string doorName1, doorName2, doorName3, doorName4, doorName5;
  public bool firstUnlocked = false;
  public bool secondUnlocked = false;

  public bool hallwayFeatureP1, pantryFeatureP1, surgeryFeatureP1, diningFeatureP1;
  public bool hallwayFeatureP2, pantryFeatureP2, surgeryFeatureP2, diningFeatureP2;

  public Image hallwayIcon, pantryIcon, surgeryIcon, diningIcon;
public List <string> RandomItems;

public string[] DoorNames = {"exit","pantry", "dining hall", "hallway", "surgery room"};

public string doorHeading, doorText;

[SerializeField] public GameObject Player1Slot4, Player2Slot4; 

  void Awake(){
      Instance = this;
        ShuffleDoors();
        doorName1 = DoorNames[0];
        doorName2 = DoorNames[1];
        doorName3 = DoorNames[2];
        doorName4 = DoorNames[3];
        doorName5 = DoorNames[4];

        SetItemsToDoors();

    if (keyItems1[0] == keyItems1[1] || keyItems2[0] == keyItems2[1] || keyItems3[0] == keyItems3[1] || keyItems4[0] == keyItems4[1] || keyItems5[0] == keyItems5[1]){
        Debug.Log("a door has the same items");
        SetItemsToDoors();
        // solange bis es halt passt
      }


  }

  

    
  public void DoorCollision(){
    AudioManager.Instance.Play("door");
     UIManager.Instance.TextToWrite = MenuManager.Instance.DoorNameText.GetComponent<Text>();
     
    
      // SETUPS FOR EACH DOOR
        if (lastVisitedDoor == "door1"){
            UIManager.Instance.messageText = "This is Door 1";
       UIManager.Instance.TextIsPlaying = true;
            SetupDoor(keyItems1, doorName1);
        } else if (lastVisitedDoor == "door2"){
            UIManager.Instance.messageText = "This is Door 2";
       UIManager.Instance.TextIsPlaying = true;
            SetupDoor(keyItems2, doorName2);
        }  else if (lastVisitedDoor == "door3"){
            UIManager.Instance.messageText = "This is Door 3";
       UIManager.Instance.TextIsPlaying = true;
           SetupDoor(keyItems3, doorName3);
        } else if (lastVisitedDoor == "door4"){
            UIManager.Instance.messageText = "This is Door 4";
       UIManager.Instance.TextIsPlaying = true;
            SetupDoor(keyItems4, doorName4);
        } else if (lastVisitedDoor == "door5"){
            UIManager.Instance.messageText = "This is Door 5";
       UIManager.Instance.TextIsPlaying = true;
            SetupDoor(keyItems5, doorName5);
        }
    // ROTATING AND SHOWING MODAL AND HIDING ITEMS
     MenuManager.Instance.ShowDoorModal();
     
  }

public void ShuffleDoors() {
         for (int i = 0; i < DoorNames.Length; i++) {
             int rnd = Random.Range(0, DoorNames.Length);
             var tempGO = DoorNames[rnd];
             DoorNames[rnd] = DoorNames[i];
             DoorNames[i] = tempGO;
         }
     }


public void SetupDoor(string[] keyItem, string doorName){
// SETUP
firstUnlocked = false;
secondUnlocked = false;
MenuManager.Instance.doorFoundModal.SetActive(false);

// Name from doorX to ACTUALDOORNAME
            lastVisitedDoor= doorName;
            string itemOne  = keyItem[0];
            string itemToHaveNext = keyItem[1];

// CHECK ITEMS PLAYER ONE
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          MenuManager.Instance.RotateModalsToPlayer1();
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (itemOne))   // waittime
             {
                 firstUnlocked = true;

             } 

             if ( x.Equals (itemToHaveNext)  && firstUnlocked == true) 
             {
                 secondUnlocked = true;
             } 
         }
      }
    
    // CHECK ITEMS PLAYER TWO
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        MenuManager.Instance.RotateModalsToPlayer2();
        foreach (string x in InventoryManager.Instance.inventoryPlayerTwo)
         {
             if (x.Equals (itemOne))
             {
                 firstUnlocked = true;
             } 

             if (x.Equals (itemToHaveNext) && firstUnlocked == true)
             {
                 secondUnlocked = true;
             } 
         }
    }




// DOORFEATURES
    // EXIT : YOU HAVE WON
    if(doorName == "exit"){ 
        if( firstUnlocked == true){
            UIManager.Instance.messageText = "This is the exit";
            UIManager.Instance.TextIsPlaying = true;
        }
        if( secondUnlocked == true){
            doorHeading = "The Exit";
            doorText = "You see a bright light and walk towards it. It looks like you have made it! This is the sunlight. That sunlight, you have dreamed of for such a long time. The one that gave you pure happiness as a kid and lets you think about times where everything was alright. Maybe now you can go back to feel this happiness again and become a better person to yourself.The bright light is fading and you can vaguely see the doctor pointing at your eyes with a flashlight. Seems like you faded out. You look around and see these same walls you've been seeing for so long. You are still in your cell, you always have been..WANNA PLAY AGAIN? :)";
            GameManager.Instance.GameState = GameState.GameWon;
        }
    } 
    // PANTRY: OPPONENT IS POISONED
    else if(doorName == "pantry"){ 
        if (firstUnlocked == true){
            UIManager.Instance.messageText = "This is the pantry";
            UIManager.Instance.TextIsPlaying = true;
        }
        if( secondUnlocked == true){
            MenuManager.Instance.doorFoundModalImage.sprite = pantryIcon.sprite;
            doorHeading = "Pantry";
            doorText = "The pantry is normally a nice place. I mean.. FOOD. But it looks like nobody was here for a while. There is mold everywhere. Even the mold has another layer of mold. You find a not so okay but okay enough looking apple and offer it to your opponent.It wasn't the best idea to be honest. Your opponent is very weak and waits two turns until the apple is fully digested.";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
                pantryFeatureP1 = true;
                MenuManager.Instance.pl1PantryFeature.sprite = pantryIcon.sprite;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
                pantryFeatureP2 = true;
                MenuManager.Instance.pl2PantryFeature.sprite = pantryIcon.sprite;
            }
        }
    } 
    // DINING: YOU CAN CARRY MORE ITEMS
    else if(doorName == "dining hall"){ 
        if (firstUnlocked == true){
            UIManager.Instance.messageText = "This is the dining hall";
            UIManager.Instance.TextIsPlaying = true;
        }
        if( secondUnlocked == true){
            MenuManager.Instance.doorFoundModalImage.sprite = diningIcon.sprite;
            doorHeading = "Dining Hall";
            doorText = "The dining hall glows with the best food you have ever seen and nobody is even here. Go ahead and eat as much as you can! You gained a lot of power. Therefore you can carry one more item with you. Yay!";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
            MenuManager.Instance.ShowAdditionalInventory1();
            InventoryManager.Instance.inventoryPlayerOne.Add("");
            InventoryManager.Instance.slotsPlayerOne.Add(Player1Slot4);
            InventoryManager.Instance.isFullPlayerOne.Add(false);
            diningFeatureP1 = true;
            MenuManager.Instance.pl1DiningFeature.sprite = diningIcon.sprite;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
            MenuManager.Instance.ShowAdditionalInventory2();
            InventoryManager.Instance.inventoryPlayerTwo.Add("");
            InventoryManager.Instance.slotsPlayerTwo.Add(Player2Slot4);
            InventoryManager.Instance.isFullPlayerTwo.Add(false);
            diningFeatureP2 = true;
            MenuManager.Instance.pl2DiningFeature.sprite = diningIcon.sprite;
        }
        }
    } 
    // HALLWAY: YOU CAN WALK MORE 
    else if(doorName == "hallway"){ 
        if (firstUnlocked == true){
            UIManager.Instance.messageText = "This is the hallway";
            UIManager.Instance.TextIsPlaying = true;
        }
        if( secondUnlocked == true){
            MenuManager.Instance.doorFoundModalImage.sprite = hallwayIcon.sprite;
            doorHeading = "Hallway";
            doorText = " You found the hallway! It seems to be the longest distance you have ever seen. There are so many doors! As you want to check if every door is locked, you walk really fast to see if a door is the exit. No chance.. But at least you have found out that you can walk really far! +1 on your walking range";
            if (GameManager.Instance.GameState == GameState.Player1Turn && secondUnlocked == true){
            Player1.Instance.walkingDistance =4;
            hallwayFeatureP1 = true;
            MenuManager.Instance.pl1HallwayFeature.sprite = hallwayIcon.sprite;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn && secondUnlocked == true){
            Player2.Instance.walkingDistance =4;
            hallwayFeatureP2 = true;
            MenuManager.Instance.pl2HallwayFeature.sprite = hallwayIcon.sprite;
        }
        } 
    } 
    // SURGERY ROOM: OPPONENT DROPS ITEM
    else if(doorName == "surgery room"){ 
        if (firstUnlocked == true){
        UIManager.Instance.messageText = "This is the surgery room";
        UIManager.Instance.TextIsPlaying = true;
        }
        if( secondUnlocked == true){
            MenuManager.Instance.doorFoundModalImage.sprite = surgeryIcon.sprite;
            doorHeading = "Surgery Room";
            doorText = "You have found the surgery room! It looks quite weird in here.. But you find some utensils, which may help you knock out your opponent. Maybe you can cut off your opponent's arm. As you run towards him you notice that the knife isn't even sharp in any kind of way. It doesn't hurt your opponent at all. Now you are fighting like two girls in puberty. Nevermind. At least your opponent has lost all the items s/he had.";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
            // InventoryManager.Instance.DropOneItem(Player2.Instance);
            surgeryFeatureP1 = true;
            MenuManager.Instance.pl1SurgeryFeature.sprite = surgeryIcon.sprite;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
            // InventoryManager.Instance.DropOneItem(Player1.Instance);
            surgeryFeatureP2 = true;
            MenuManager.Instance.pl2SurgeryFeature.sprite = surgeryIcon.sprite;
        }
        }
        
    }

// CHANGE LEFT DOOR ITEM IMAGE
    ItemManager.Instance.ChangeDoorItemImageLeft(keyItem[0]);
            

    // CHANGE RIGHT DOOR ITEM IMAGE
    if (firstUnlocked == true){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().fontSize = 18;
        ItemManager.Instance.ChangeDoorItemImageRight(keyItem[1]);
        if (secondUnlocked == true){
            MenuManager.Instance.AnimateDoorModal("DoorModalCheckRight");
            MenuManager.Instance.UpdateDoorFoundModal(doorHeading, doorText);
            } else {
            MenuManager.Instance.AnimateDoorModal("DoorModalCheckLeft");

    }
    } else if (firstUnlocked == false){
                ItemManager.Instance.ChangeDoorItemImageRight("none");
                MenuManager.Instance.AnimateDoorModal("DoorModalCheckRight"); 
                doorHeading = "Surgery Room";
            doorText = "You have found the surgery room! It looks quite weird in here.. But you find some utensils, which may help you knock out your opponent. Maybe you can cut off your opponent's arm. As you run towards him you notice that the knife isn't even sharp in any kind of way. It doesn't hurt your opponent at all. Now you are fighting like two girls in puberty. Nevermind. At least your opponent has lost all the items s/he had.";
            MenuManager.Instance.UpdateDoorFoundModal(doorHeading, doorText);
            Debug.Log ("hello");
    }
}

public void SetItemsToDoors(){
  for (int i = 0; i < 2; i++)
      {
        
        RandomItems.Add("Item1");
        RandomItems.Add("Item2");
        RandomItems.Add("Item3");
        RandomItems.Add("Item4");
        RandomItems.Add("Item5");
        RandomItems.Add("Item6");
        RandomItems.Add("Item7");


      keyItems1[i] = RandomItems[Random.Range(0,RandomItems.Count)];
      RandomItems.Remove(keyItems1[i]);
      keyItems2[i] = RandomItems[Random.Range(0,RandomItems.Count)];
      RandomItems.Remove(keyItems2[i]);
      keyItems3[i] = RandomItems[Random.Range(0,RandomItems.Count)];
      RandomItems.Remove(keyItems3[i]);
      keyItems4[i] = RandomItems[Random.Range(0,RandomItems.Count)];
      RandomItems.Remove(keyItems4[i]);
      keyItems5[i] = RandomItems[Random.Range(0,RandomItems.Count)];
      RandomItems.Remove(keyItems5[i]);


      RandomItems.Clear();
      }
}

}
