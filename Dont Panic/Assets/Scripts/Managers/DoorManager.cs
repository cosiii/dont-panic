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
public string[] RandomItems = {"Item1","Item2", "Item3", "Item4", "Item5", "Item6", "Item7" };

public string[] DoorNames = {"exit","pantry", "dining hall", "hallway", "surgery room"};

[SerializeField] public GameObject Player1Slot4, Player2Slot4; 


  void Awake(){
      Instance = this;
        Shuffle();
        doorName1 = DoorNames[0];
        doorName2 = DoorNames[1];
        doorName3 = DoorNames[2];
        doorName4 = DoorNames[3];
        doorName5 = DoorNames[4];

      for (int i = 0; i < 2; i++)
      {
      keyItems1[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems2[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems3[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems4[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems5[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      }
  }
public void Shuffle() {
         for (int i = 0; i < DoorNames.Length; i++) {
             int rnd = Random.Range(0, DoorNames.Length);
             var tempGO = DoorNames[rnd];
             DoorNames[rnd] = DoorNames[i];
             DoorNames[i] = tempGO;
         }
     }
    
  public void DoorCollision(){

      // SETUPS FOR EACH DOOR
        if (lastVisitedDoor == "door1"){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is Door 1";
            SetupDoor(keyItems1, doorName1);
            ShowDoorFeature(doorName1);
        } else if (lastVisitedDoor == "door2"){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is Door 2";
            SetupDoor(keyItems2, doorName2);
            ShowDoorFeature(doorName2);
        }  else if (lastVisitedDoor == "door3"){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is Door 3";
           SetupDoor(keyItems3, doorName3);
           ShowDoorFeature(doorName3);
        } else if (lastVisitedDoor == "door4"){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is Door 4";
            SetupDoor(keyItems4, doorName4);
            ShowDoorFeature(doorName4);
        } else if (lastVisitedDoor == "door5"){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is Door 5";
            SetupDoor(keyItems5, doorName5);
            ShowDoorFeature(doorName5);
        }
      
    // ROTATING AND SHOWING MODAL AND HIDING ITEMS
    // buggt wenn man ein zweites mal aufs door kommt
     MenuManager.Instance.ShowDoorModal();
     
  }

public void ShowDoorFeature(string doorName){

    if (firstUnlocked == true){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().fontSize = 18;
    } else if (secondUnlocked == true){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().fontSize = 12;
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }


    // EXIT : YOU HAVE WON
    if(doorName == "exit"){ 
        if( firstUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is the exit";
        }
        if( secondUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "You see a bright light and walk towards it. It looks like you have made it! This is the sunlight. That sunlight, you have dreamed of for such a long time. The one that gave you pure happiness as a kid and lets you think about times where everything was alright. Maybe now you can go back to feel this happiness again and become a better person to yourself.The bright light is fading and you can vaguely see the doctor pointing at your eyes with a flashlight. Seems like you faded out. You look around and see these same walls you've been seeing for so long. You are still in your cell, you always have been..WANNA PLAY AGAIN? :)";
            GameManager.Instance.GameState = GameState.GameWon;
        }
    } 
    // PANTRY: OPPONENT IS POISONED
    else if(doorName == "pantry"){ 
        if (firstUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is the pantry";
        }
        if( secondUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "The pantry is normally a nice place. I mean.. FOOD. But it looks like nobody was here for a while. There is mold everywhere. Even the mold has another layer of mold. You find a not so okay but okay enough looking apple and offer it to your opponent.It wasn't the best idea to be honest. Your opponent is very weak and waits two turns until the apple is fully digested.";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
                pantryFeatureP1 = true;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
            pantryFeatureP2 = true;
            }
        }
    } 
    // DINING: YOU CAN CARRY MORE ITEMS
    else if(doorName == "dining hall"){ 
        if (firstUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is the dining hall";
        }
        if( secondUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "The dining hall glows with the best food you have ever seen and nobody is even here. Go ahead and eat as much as you can! You gained a lot of power. Therefore you can carry one more item with you. Yay!";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
            MenuManager.Instance.ShowAdditionalInventory1();
            InventoryManager.Instance.inventoryPlayerOne.Add("");
            InventoryManager.Instance.slotsPlayerOne.Add(Player1Slot4);
            InventoryManager.Instance.isFullPlayerOne.Add(false);
            diningFeatureP1 = true;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
            MenuManager.Instance.ShowAdditionalInventory2();
            InventoryManager.Instance.inventoryPlayerTwo.Add("");
            InventoryManager.Instance.slotsPlayerTwo.Add(Player2Slot4);
            InventoryManager.Instance.isFullPlayerTwo.Add(false);
            diningFeatureP2 = true;
        }
        }
    } 
    // HALLWAY: YOU CAN WALK MORE 
    else if(doorName == "hallway"){ 
        if (firstUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is the hallway";
        }
        if( secondUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = " You found the hallway! It seems to be the longest distance you have ever seen. There are so many doors! As you want to check if every door is locked, you walk really fast to see if a door is the exit. No chance.. But at least you have found out that you can walk really far! +1 on your walking range";
            if (GameManager.Instance.GameState == GameState.Player1Turn && secondUnlocked == true){
            Player1.Instance.walkingDistance =4;
            hallwayFeatureP1 = true;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn && secondUnlocked == true){
            Player2.Instance.walkingDistance =4;
            hallwayFeatureP2 = true;
        }
        } 
    } 
    // OPPONENT DROPS ITEM
    else if(doorName == "surgery room"){ 
        if (firstUnlocked == true){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "This is the surgery room";
        }
        if( secondUnlocked == true){
            MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "You have found the surgery room! It looks quite weird in here.. But you find some utensils, which may help you knock out your opponent. Maybe you can cut off your opponent's arm. As you run towards him you notice that the knife isn't even sharp in any kind of way. It doesn't hurt your opponent at all. Now you are fighting like two girls in puberty. Nevermind. At least your opponent has lost all the items s/he had.";
            if (GameManager.Instance.GameState == GameState.Player1Turn){
            InventoryManager.Instance.DropOneItem(Player2.Instance);
            surgeryFeatureP1 = true;
            } else if (GameManager.Instance.GameState == GameState.Player2Turn){
            InventoryManager.Instance.DropOneItem(Player1.Instance);
            surgeryFeatureP2 = true;
        }
        }
        
    }
}
public void SetupDoor(string[] keyItem, string doorName){
            // Name from doorX to ACTUALDOORNAME
            lastVisitedDoor= doorName;
            SearchItem(keyItem[0], keyItem[1]);
            ItemManager.Instance.ChangeDoorItemImageLeft(keyItem[0]);
            // only show second item and doorName when FiRST ONE IS IN INVENTORY
            if(firstUnlocked == true){
                ItemManager.Instance.ChangeDoorItemImageRight(keyItem[1]);
            } else if (firstUnlocked == false){
                ItemManager.Instance.ChangeDoorItemImageRight("none");
    }
}

  public void SearchItem(string itemOne, string itemToHaveNext){
    // CHECK ITEMS PLAYER ONE

    // einheitlicher machen
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          firstUnlocked = false;
          secondUnlocked = false;

          // ROTATE MODAL
          MenuManager.Instance.RotateModalsToPlayer1();

          // SEARCH FOR THE ITEMS
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (itemOne))
             {
                 Debug.Log("you, player one has this item, the next item is " + itemToHaveNext);
                 
                 firstUnlocked = true;
             } 

             if (x.Equals (itemToHaveNext) && x.Equals (itemOne)) 
             {
                 secondUnlocked = true;
             } 
         }
      }
    
    // CHECK ITEMS PLAYER TWO
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        firstUnlocked = false;
        secondUnlocked = false;
        MenuManager.Instance.RotateModalsToPlayer2();

          foreach (string x in InventoryManager.Instance.inventoryPlayerTwo)
         {
             if (x.Equals (itemOne))
             {
                 Debug.Log("you, player two has this item, the next item is " + itemToHaveNext);
                 firstUnlocked = true;
             } 

             if (x.Equals (itemToHaveNext) && firstUnlocked == true)
             {
                 Debug.Log("you have both items " );
                 secondUnlocked = true;
             } 
         }
    }
  }
}
