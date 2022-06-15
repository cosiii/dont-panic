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
public string lastDestroyedItem, lastNotDestroyedItem;

public bool itemUnderPlayer1, itemUnderPlayer2, ItemTransferred1, ItemTransferred2;

public Tile itemUnderPlayer1Tile, itemUnderPlayer2Tile;

public string lastDroppedItem;
public bool inventoryIsFullPlayerOne;
public bool inventoryIsFullPlayerTwo;

public GameObject inventoryPoint;
List <int> nineTiles =  new List<int>{-1, 0, 1};

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){

if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" ){
    // PLAYER ONE
    MenuManager.Instance.RotateModalsToPlayer1();
    for (int i = 0; i < slotsPlayerOne.Count; i++)
    {
        if(isFullPlayerOne[i] == false ){ // item can be added to inventory
            // parented to slots[i]
            Instantiate(inventoryPoint, slotsPlayerOne[i].transform, false);
            AnimationManager.Instance.SlotToAnimate = slotsPlayerOne[i];
            isFullPlayerOne[i] = true;
            inventoryPlayerOne[i] = lastDestroyedItem;
            break;
        }
    }

    Debug.Log("Item collected by Player 1");

} else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2"){ 
    //PLAYER TWO
    MenuManager.Instance.RotateModalsToPlayer2(); 
    for (int i = 0; i < slotsPlayerTwo.Count; i++)
    {
        if(isFullPlayerTwo[i] == false){ // item can be added to inventory
            // parented to slots[i]
            Instantiate(inventoryPoint, slotsPlayerTwo[i].transform, false);
            AnimationManager.Instance.SlotToAnimate = slotsPlayerTwo[i];
            isFullPlayerTwo[i] = true;
            inventoryPlayerTwo[i] = lastDestroyedItem;
            break;
        }
    }

    Debug.Log("Item collected by Player 2");
}

    ItemManager.Instance.ChangeModal();
    AudioManager.Instance.Play("collect");
    MenuManager.Instance.AnimateItemModal();
    // Change und animate zsmfügen?

}


public void DropOneItem(BasePlayer player, List <GameObject> slots, List <bool> isFull, List <string> inventory, bool inventoryIsFull){
    // RANDOM TILE POS AROUNG THE PLAYER
    int randomx = nineTiles[Random.Range(0,nineTiles.Count)];
    int randomy = nineTiles[Random.Range(0,nineTiles.Count)];

    // IN CASE of 0 0
    if (randomx == 0 && randomy == 0){
        randomx = 1;
    }

    // die tiles ausenrum walkabel
    var spawnTileAroundPlayer = GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy);

    // if the item has space
    if(GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit == null){
        //Debug.Log("it is walkable here");
    } else if(GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit != null){
        Debug.Log("it is not walkable here");
        // respawn items
    }
    
        for (int i = 0; i < slots.Count; i++)
        { 
            if(isFull[slots.Count-1 -i] == true){ 
            Debug.Log("its true player 1 has items");
            Destroy(slots[slots.Count-1 -i].transform.GetChild(0).gameObject);
            isFull[slots.Count-1 -i] = false;
            lastDroppedItem = inventory[slots.Count-1 -i];
            inventory[slots.Count-1 -i] = "";
            inventoryIsFull = false;
            break;
            }
         }

    var spawnedItem = UnitManager.Instance.Item1;
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
}
}
