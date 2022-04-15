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

        if (lastVisitedDoor == "door1"){
            Debug.Log(doorName1 + keyItems1[0]);
            SearchItem(keyItems1[0], keyItems1[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItems1[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItems1[1];
            MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName1;
        } else if (lastVisitedDoor == "door2"){
            Debug.Log(doorName2 + keyItems2[0]);
            SearchItem(keyItems2[0],keyItems2[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItems2[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItems2[1];
            MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName2;
        }  else if (lastVisitedDoor == "door3"){
            Debug.Log(doorName3 + keyItems3[0]);
            SearchItem(keyItems3[0], keyItems3[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItems3[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItems3[1];
            MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName3;
        } else if (lastVisitedDoor == "door4"){
            Debug.Log(doorName4 + keyItems4[0]);
            SearchItem(keyItems4[0], keyItems4[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItems4[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItems4[1];
            MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName4;
        } else if (lastVisitedDoor == "door5"){
            Debug.Log(doorName5 + keyItems5[0]);
            SearchItem(keyItems5[0], keyItems5[1]);
            MenuManager.Instance.Item1Object.GetComponentInChildren<Text>().text = keyItems5[0];
            MenuManager.Instance.Item2Object.GetComponentInChildren<Text>().text = keyItems5[1];
            MenuManager.Instance.doorNameObject.GetComponentInChildren<Text>().text = doorName5;
        }
      
     MenuManager.Instance.ShowDoorModal();
  }


  public void SearchItem(string stringToCheck, string itemToHaveNext){
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (stringToCheck))
             {
                 Debug.Log("you, player one has this item, the next item is " + itemToHaveNext);
             }

            /* if (x.Equals (itemToHaveNext)) erst wenn man das erste auch hat, einfach in if oben
             {
                 Debug.Log("you also have the next item");
             } */
         }
      }
      
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        foreach (string x in InventoryManager.Instance.inventoryPlayerTwo)
         {
             if (x.Equals (stringToCheck))
             {
                 Debug.Log("you, player two has this item,  the next item is" + itemToHaveNext);
             }
         }
    }
  }
}
