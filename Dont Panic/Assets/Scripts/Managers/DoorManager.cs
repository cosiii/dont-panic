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

      // TAKE JUST ONE AND LET IT OUT OF aaRAy
      doorName1 = DoorNames[Random.Range(0,DoorNames.Length )];
      doorName2 = DoorNames[Random.Range(0,DoorNames.Length )];
      doorName3 = DoorNames[Random.Range(0,DoorNames.Length )];
      doorName4 = DoorNames[Random.Range(0,DoorNames.Length )];
      doorName5 = DoorNames[Random.Range(0,DoorNames.Length )];

      for (int i = 0; i < 2; i++)
      {
      keyItems1[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems2[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems3[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems4[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      keyItems5[i] = RandomItems[Random.Range(0,RandomItems.Length )];
      }
  }

  public void DoorCollision(){

      // SETUPS FOR EACH DOOR
        if (lastVisitedDoor == "door1"){
            SetupDoor(keyItems1, doorName1);
        } else if (lastVisitedDoor == "door2"){
            SetupDoor(keyItems2, doorName2);
        }  else if (lastVisitedDoor == "door3"){
           SetupDoor(keyItems3, doorName3);
        } else if (lastVisitedDoor == "door4"){
            SetupDoor(keyItems4, doorName4);
        } else if (lastVisitedDoor == "door5"){
            SetupDoor(keyItems5, doorName5);
        }
      
    // SHOWING MODAL AND HIDING ITEMS
     MenuManager.Instance.ShowDoorModal();
  }

public void SetupDoor(string[] keyItem, string doorName){
            Debug.Log(doorName + keyItem[0]);
            SearchItem(keyItem[0], keyItem[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItem[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItem[1];
            // only show second item and doorName when FiRST ONE IS IN INVENTORY
            if(firstUnlocked == true){
                MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName;
                MenuManager.Instance.ShowSecondItem();
            } else if (firstUnlocked == false){
                MenuManager.Instance.HideSecondItem();
    }
}

  public void SearchItem(string itemOne, string itemToHaveNext){
    // CHECK ITEMS PLAYER ONE
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          firstUnlocked = false;
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (itemOne))
             {
                 Debug.Log("you, player one has this item, the next item is " + itemToHaveNext);
                 firstUnlocked = true;
             } 

             if (x.Equals (itemToHaveNext) && firstUnlocked == true)
             {
                 Debug.Log("you have both items " );
             } 
         }
      }
    
    // CHECK ITEMS PLAYER TWO
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        firstUnlocked = false;
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
             } 
         }
    }
  }
}
