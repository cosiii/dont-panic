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
public string[] RandomItems = {"Item1","Item2", "Item3", "Item4", "Item5", "Item6", "Item7" };

public string[] DoorNames = {"exit","pantry", "dining hall", "hallway", "surgery room"};
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
            SetupDoor(keyItems1, doorName1);
            ShowDoorFeature(doorName1);
        } else if (lastVisitedDoor == "door2"){
            SetupDoor(keyItems2, doorName2);
            ShowDoorFeature(doorName2);
        }  else if (lastVisitedDoor == "door3"){
           SetupDoor(keyItems3, doorName3);
           ShowDoorFeature(doorName3);
        } else if (lastVisitedDoor == "door4"){
            SetupDoor(keyItems4, doorName4);
            ShowDoorFeature(doorName4);
        } else if (lastVisitedDoor == "door5"){
            SetupDoor(keyItems5, doorName5);
            ShowDoorFeature(doorName5);
        }
      
    // ROTATING AND SHOWING MODAL AND HIDING ITEMS
    // buggt wenn man ein zweites mal aufs door kommt
     MenuManager.Instance.ShowDoorModal();
  }

public void ShowDoorFeature(string doorName){
    if(doorName == "exit"){
    } else if(doorName == "pantry"){
    } else if(doorName == "dining hall"){
    } else if(doorName == "hallway"){
    } else if(doorName == "surgery room"){
    }
}
public void SetupDoor(string[] keyItem, string doorName){
            // Name from doorX to ACTUALDOORNAME
            lastVisitedDoor= doorName;
            // Debug.Log(doorName + keyItem[0]);
            SearchItem(keyItem[0], keyItem[1]);
            ItemManager.Instance.ChangeDoorItemImageLeft(keyItem[0]);
            // only show second item and doorName when FiRST ONE IS IN INVENTORY
            if(firstUnlocked == true){
                ItemManager.Instance.ChangeDoorItemImageRight(keyItem[1]);
                firstUnlocked = false;
            } else if (firstUnlocked == false){
                ItemManager.Instance.ChangeDoorItemImageRight("none");
    }
}

  public void SearchItem(string itemOne, string itemToHaveNext){
    // CHECK ITEMS PLAYER ONE

    // einheitlicher machen
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          firstUnlocked = false;
          // ROTATE MODAL
          MenuManager.Instance.RotateModalsToPlayer1();
          // SEARCH FOR THE ITEMS
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (itemOne))
             {
                 Debug.Log("you, player one has this item, the next item is " + itemToHaveNext);
                 firstUnlocked = true;
                 MenuManager.Instance.doorTextObject.GetComponentInChildren<Text>().text = "You have the first Item! Go search for the second to open the door";
                 MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = lastVisitedDoor;
             } 

             if (x.Equals (itemToHaveNext) && x.Equals (itemOne)) // mal schauen
             {
                 Debug.Log("you have both items " );
                 MenuManager.Instance.doorTextObject.GetComponentInChildren<Text>().text = "congrats! you have both items";
                 MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = lastVisitedDoor;
             } 
         }
      }
    
    // CHECK ITEMS PLAYER TWO
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        firstUnlocked = false;
        MenuManager.Instance.RotateModalsToPlayer2();
          foreach (string x in InventoryManager.Instance.inventoryPlayerTwo)
         {
             if (x.Equals (itemOne))
             {
                 Debug.Log("you, player two has this item, the next item is " + itemToHaveNext);
                 firstUnlocked = true;
                 MenuManager.Instance.doorTextObject.GetComponentInChildren<Text>().text = "You have the first Item! Go search for the second to open the door";
                 MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = lastVisitedDoor;
             } 

             if (x.Equals (itemToHaveNext) && firstUnlocked == true)
             {
                 Debug.Log("you have both items " );
                 MenuManager.Instance.doorTextObject.GetComponentInChildren<Text>().text = "congrats! you have both items";
                 MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = lastVisitedDoor;
             } 
         }
    }
  }
}
