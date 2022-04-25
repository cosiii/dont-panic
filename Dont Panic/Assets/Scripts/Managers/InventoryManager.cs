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
    Debug.Log("item dropped");

     //PLAYER ONE
    for (int i = 0; i < slotsPlayerOne.Length; i++)
    {
        if(isFullPlayerOne[slotsPlayerOne.Length-1 -i] == true ){ // item can be added to inventory
            // parented to slots[i]
            Destroy(slotsPlayerOne[slotsPlayerOne.Length-1 -i].transform.GetChild(0).gameObject);
            // Destroy(slotsPlayerOne[i], 0f); zwei punkte weg
            isFullPlayerOne[slotsPlayerOne.Length-1 -i] = false;
            lastDroppedItem = inventoryPlayerOne[slotsPlayerOne.Length-1 -i];
            inventoryPlayerOne[slotsPlayerOne.Length-1 -i] = "";
            break;
        }
    }

    if (lastDroppedItem == "Item4"){
        // spawning item
            var spawnedItem = Instantiate(UnitManager.Instance.Item4);
            // get the tile of the item from GridManager
            var randomSpawnTile = GridManager.Instance.GetSpawnTile(Player1.Instance.posx,Player1.Instance.posy);
            randomSpawnTile.SetUnit(spawnedItem);
            Destroy(Player1.Instance.gameObject);


           // char cx =UnitManager.Instance.Item4.OccupiedTile.name[5];
            //char cy = UnitManager.Instance.Item4.OccupiedTile.name[7];

            Instantiate(UnitManager.Instance.Player1);
            //Tile.Instance.SetUnit(UnitManager.Instance.SelectedPlayer);
            //SpawnTile.SetUnit(spawnedPlayer);
    }

}
}
