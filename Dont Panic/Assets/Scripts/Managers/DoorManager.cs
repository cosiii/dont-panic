using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
  public static DoorManager Instance;
  public string lastVisitedDoor;

  public string[] keyItems1, keyItems2, keyItems3, keyItems4, keyItems5;
  
public string[] RandomItems = {"Item1","Item2", "Item3", "Item4", "Item5", "Item6", "Item7" };
  void Awake(){
      Instance = this;
      for (int i = 0; i < 2; i++)
      {
      keyItems1[i] = RandomItems[Random.Range(0,RandomItems.Length)];
      keyItems2[i] = RandomItems[Random.Range(0,RandomItems.Length)];
      keyItems3[i] = RandomItems[Random.Range(0,RandomItems.Length)];
      keyItems4[i] = RandomItems[Random.Range(0,RandomItems.Length)];
      keyItems5[i] = RandomItems[Random.Range(0,RandomItems.Length)];
      }
  }

  public void DoorCollision(){
        //Debug.Log(lastVisitedDoor);
         


        if (lastVisitedDoor == "door1"){
            Debug.Log("door1" + keyItems1[0]);
            SearchItem(keyItems1[0], keyItems1[1]);
        } else if (lastVisitedDoor == "door2"){
            Debug.Log("door2" + keyItems2[0]);
            SearchItem(keyItems2[0],keyItems2[1]);
        }  else if (lastVisitedDoor == "door3"){
            Debug.Log("door3" + keyItems3[0]);
            SearchItem(keyItems3[0], keyItems3[1]);
        } else if (lastVisitedDoor == "door4"){
            Debug.Log("door4" + keyItems4[0]);
            SearchItem(keyItems4[0], keyItems4[1]);
        } else if (lastVisitedDoor == "door5"){
            Debug.Log("door5" + keyItems5[0]);
            SearchItem(keyItems5[0], keyItems5[1]);
        }
      // jedem Door zwei Items zuordnen, evtl als String ausgeben
      
  }
  public void SearchItem(string stringToCheck, string itemToHaveNext){
      if(GameManager.Instance.GameState == GameState.Player1Turn){
          foreach (string x in InventoryManager.Instance.inventoryPlayerOne)
         {
             if (x.Equals (stringToCheck))
             {
                 Debug.Log("you, player one has this item, the next item is " + itemToHaveNext);
             }

             if (x.Equals (itemToHaveNext))
             {
                 Debug.Log("xou also have the next item");
             }
         }
      }
      
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        foreach (string x in InventoryManager.Instance.inventoryPlayerTwo)
         {
             if (x.Equals (stringToCheck))
             {
                 Debug.Log("you, player two has this item");
             }
         }
    }
  }
}
