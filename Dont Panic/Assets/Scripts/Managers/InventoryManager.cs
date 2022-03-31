using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
 public static InventoryManager Instance;
 public bool[] isFullPlayerOne;
 public bool[] isFullPlayerTwo;
 public GameObject[] slotsPlayerOne;
  public GameObject[] slotsPlayerTwo;

 public GameObject inventoryPoint;

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){



Debug.Log("item picked up by " + MenuManager.Instance.selectedPlayerObject.GetComponentInChildren<Text>().text);

if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1"){  //PLAYER ONE
    for (int i = 0; i < slotsPlayerOne.Length; i++)
    {
        if(isFullPlayerOne[i] == false){ // item can be added to inventory
            // parented to slots[i]
            Instantiate(inventoryPoint, slotsPlayerOne[i].transform, false);
            isFullPlayerOne[i] = true;
            break;
        }
    }
} else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2"){ //PLAYER TWO
    for (int i = 0; i < slotsPlayerTwo.Length; i++)
    {
        if(isFullPlayerTwo[i] == false){ // item can be added to inventory
            // parented to slots[i]
            Instantiate(inventoryPoint, slotsPlayerTwo[i].transform, false);
            isFullPlayerTwo[i] = true;
            break;
        }
    }
}


 }

}
