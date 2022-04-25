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
public string[] inventoryPlayerOne;

public string[] inventoryPlayerTwo;


public string lastDestroyedItem;

public string lastDroppedItem;
public bool inventoryIsFullPlayerOne;
public bool inventoryIsFullPlayerTwo;

public GameObject inventoryPoint;

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){
if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1"){  //PLAYER ONE
    for (int i = 0; i < slotsPlayerOne.Length; i++)
    {
        if(isFullPlayerOne[i] == false ){ // item can be added to inventory
            // parented to slots[i]
            Instantiate(inventoryPoint, slotsPlayerOne[i].transform, false);
            isFullPlayerOne[i] = true;
            inventoryPlayerOne[i] = lastDestroyedItem;
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
            inventoryPlayerTwo[i] = lastDestroyedItem;
            break;
        }
    }
}

 if(isFullPlayerOne[slotsPlayerOne.Length -1] == true){
                Debug.Log("inventory full pl1");
                inventoryIsFullPlayerOne = true;
            } 

 if(isFullPlayerTwo[slotsPlayerTwo.Length -1] == true){
                Debug.Log("inventory full pl2");
                inventoryIsFullPlayerTwo = true;
            } 
 }

public void DropItem(){

     //PLAYER ONE
    for (int i = 0; i < slotsPlayerOne.Length; i++)
    {
        
        if(isFullPlayerOne[slotsPlayerOne.Length-1 -i] == true && GameManager.Instance.GameState == GameState.Player1Turn ){ // item can be added to inventory
            
            Destroy(slotsPlayerOne[slotsPlayerOne.Length-1 -i].transform.GetChild(0).gameObject);
            isFullPlayerOne[slotsPlayerOne.Length-1 -i] = false;
            lastDroppedItem = inventoryPlayerOne[slotsPlayerOne.Length-1 -i];
            inventoryPlayerOne[slotsPlayerOne.Length-1 -i] = "";
            inventoryIsFullPlayerOne = false;
            break;
        }
    }

    //PLAYER TWO
    for (int i = 0; i < slotsPlayerTwo.Length; i++)
    {
        if(isFullPlayerTwo[slotsPlayerTwo.Length-1 -i] == true && GameManager.Instance.GameState == GameState.Player2Turn ){ // item can be added to inventory
            // parented to slots[i]
            Destroy(slotsPlayerTwo[slotsPlayerTwo.Length-1 -i].transform.GetChild(0).gameObject);
            // Destroy(slotsPlayerOne[i], 0f); zwei punkte weg
            isFullPlayerTwo[slotsPlayerTwo.Length-1 -i] = false;
            lastDroppedItem = inventoryPlayerTwo[slotsPlayerTwo.Length-1 -i];
            inventoryPlayerTwo[slotsPlayerTwo.Length-1 -i] = "";
            inventoryIsFullPlayerTwo = false;
            break;
        }
    }

    var spawnedItem = UnitManager.Instance.Item1;
    var randomSpawnTile = GridManager.Instance.GetSpawnTile(1,1);

    if (lastDroppedItem != ""){
        if (lastDroppedItem == "Item1"){
        // spawning item
        spawnedItem = Instantiate(UnitManager.Instance.Item1);
        // get the tile of the item from GridManager    
        } else if (lastDroppedItem == "Item2"){
        spawnedItem = Instantiate(UnitManager.Instance.Item2);
        } else if (lastDroppedItem == "Item3"){
        spawnedItem = Instantiate(UnitManager.Instance.Item3);
        } else if (lastDroppedItem == "Item4"){
        spawnedItem = Instantiate(UnitManager.Instance.Item4);
        } else if (lastDroppedItem == "Item5"){
        spawnedItem = Instantiate(UnitManager.Instance.Item5);
        }else if (lastDroppedItem == "Item6"){
        spawnedItem = Instantiate(UnitManager.Instance.Item6);
        }else if (lastDroppedItem == "Item7"){
        spawnedItem = Instantiate(UnitManager.Instance.Item7);
        }

        if( GameManager.Instance.GameState == GameState.Player1Turn){
         randomSpawnTile = GridManager.Instance.GetSpawnTile(Player1.Instance.posx -1,Player1.Instance.posy-1);
         if(isFullPlayerOne[0] == false){
             Debug.Log("you don't have Items to drop");
            }
        } else if( GameManager.Instance.GameState == GameState.Player2Turn){
         randomSpawnTile = GridManager.Instance.GetSpawnTile(Player2.Instance.posx -1,Player2.Instance.posy-1);
         if(isFullPlayerTwo[0] == false){
             Debug.Log("you don't have Items to drop");
            }
        }

        randomSpawnTile.SetUnit(spawnedItem);
        lastDroppedItem = "";
    }
    

}
}
