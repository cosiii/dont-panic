using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
 public static InventoryManager Instance;
 public List <bool> isFullPlayerOne;
 public List <bool> isFullPlayerTwo;
 public List <GameObject> slotsPlayerOne;
public List <GameObject> slotsPlayerTwo;
public List <string> inventoryPlayerOne;

public List <string> inventoryPlayerTwo;
public string lastDestroyedItem;

public string lastDroppedItem;
public bool inventoryIsFullPlayerOne;
public bool inventoryIsFullPlayerTwo;

public GameObject inventoryPoint;
List <int> nineTiles =  new List<int>{-1, 0, 1};

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){
if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1"){
    //PLAYER ONE
    // rotate ItemModal
    MenuManager.Instance.RotateModalsToPlayer1();
    for (int i = 0; i < slotsPlayerOne.Count; i++)
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
    // rotate itemModal
    MenuManager.Instance.RotateModalsToPlayer2();
    for (int i = 0; i < slotsPlayerTwo.Count; i++)
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

 if(isFullPlayerOne[slotsPlayerOne.Count -1] == true){
                Debug.Log("inventory full pl1");
                inventoryIsFullPlayerOne = true;
            } 

 if(isFullPlayerTwo[slotsPlayerTwo.Count -1] == true){
                Debug.Log("inventory full pl2");
                inventoryIsFullPlayerTwo = true;
            } 
 }


public void DropOneItem(BasePlayer player){

    // just set something, declared later
    var spawnedItem = UnitManager.Instance.Item1;
    int randomx;
    int randomy;
    // x != 0 && y!= 0 else redo x und y neu
    // um den player herum
    randomx = nineTiles[Random.Range(0,nineTiles.Count)];
    randomy = nineTiles[Random.Range(0,nineTiles.Count)];

    // just in case it drops on the exact same spot
    if (randomx == 0 && randomy == 0){
        randomx = 1;
    }
    // die tiles ausenrum walkabel
    var spawnTileAroundPlayer = GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy);

    if(GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit == null){
        Debug.Log("it is walkable here");
    } else if(GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit != null){
        Debug.Log("it is not walkable here");
        // respawn items
    }
    
    //PLAYER ONE
    if (player = Player1.Instance){
        if(isFullPlayerOne[0] == false){
             Debug.Log("you (Pl1) don't have Items to drop");
            } 
        
        for (int i = 0; i < slotsPlayerOne.Count; i++)
        {
            if(isFullPlayerOne[slotsPlayerOne.Count-1 -i] == true){ 
            Debug.Log("its true");
            Destroy(slotsPlayerOne[slotsPlayerOne.Count-1 -i].transform.GetChild(0).gameObject);
            isFullPlayerOne[slotsPlayerOne.Count-1 -i] = false;
            lastDroppedItem = inventoryPlayerOne[slotsPlayerOne.Count-1 -i];
            inventoryPlayerOne[slotsPlayerOne.Count-1 -i] = "";
            inventoryIsFullPlayerOne = false;
            break;
            }
         }
    }

    //PLAYER TWO
    if (player = Player2.Instance){
        if(isFullPlayerTwo[0] == false){
             Debug.Log("you (Pl2) don't have Items to drop");
            } 
        
        for (int i = 0; i < slotsPlayerTwo.Count; i++)
        {
            if(isFullPlayerTwo[slotsPlayerTwo.Count-1 -i] == true){ 
            Debug.Log("its true");
            Destroy(slotsPlayerTwo[slotsPlayerTwo.Count-1 -i].transform.GetChild(0).gameObject);
            isFullPlayerTwo[slotsPlayerTwo.Count-1 -i] = false;
            lastDroppedItem = inventoryPlayerTwo[slotsPlayerTwo.Count-1 -i];
            inventoryPlayerTwo[slotsPlayerTwo.Count-1 -i] = "";
            inventoryIsFullPlayerTwo = false;
            break;
            }
         }
    }
    

    if (lastDroppedItem != ""){
        if (lastDroppedItem == "Item1"){
        spawnedItem = Instantiate(UnitManager.Instance.Item1);
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

        spawnTileAroundPlayer.SetUnit(spawnedItem);
        lastDroppedItem = "";
    }
   // GameManager.Instance.GameState = GameState.Player2Turn;
}
}
