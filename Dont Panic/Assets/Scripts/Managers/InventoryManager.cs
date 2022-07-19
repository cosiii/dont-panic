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

public bool itemUnderPlayer1, itemUnderPlayer2;

public Tile itemUnderPlayer1Tile, itemUnderPlayer2Tile;

 // RANDOM TILE POS AROUNG THE PLAYER
    int randomx, randomy;

public string lastDroppedItem;
public bool inventoryIsFullPlayerOne;
public bool inventoryIsFullPlayerTwo;

public GameObject inventoryPoint;

int k =0;
int l =0;
List <int> nineTiles =  new List<int>{-1, 0, 1};

void Awake(){
        Instance = this;
    }

public void Update(){
}

 public void AddItemToInventory(){
if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" ){

    if(DoorManager.Instance.diningFeatureP1) k =1;
    // PLAYER ONE
    for (int i = 0; i < slotsPlayerOne.Count+ k; i++)
    {
        Debug.Log("item added, slots count: " + slotsPlayerOne.Count + " inventory player one: " + inventoryPlayerOne[2] );
        if(isFullPlayerOne[i] == false ){ 
        // ITEM WILL BE ADDED TO INVENTORY
        // parented to slots[i]
        AnimationManager.Instance.SlotToAnimate = slotsPlayerOne[i];
        Instantiate(inventoryPoint, slotsPlayerOne[i].transform, false);
        isFullPlayerOne[i] = true;
        inventoryPlayerOne[i] = lastDestroyedItem;
        break;
        }
    }

} else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2"){ 
    
    if(DoorManager.Instance.diningFeatureP2) l =1;
    //PLAYER TWO
    for (int i = 0; i < slotsPlayerTwo.Count+l; i++)
    {
        Debug.Log("item added, slots count: " + slotsPlayerOne.Count + " inventory player two: " + inventoryPlayerTwo);
        if(isFullPlayerTwo[i] == false){ 
        // ITEM WILL BE ADDED TO INVENTORY
        // parented to slots[i]
            AnimationManager.Instance.SlotToAnimate = slotsPlayerTwo[i];
            Instantiate(inventoryPoint, slotsPlayerTwo[i].transform, false);
            isFullPlayerTwo[i] = true;
            inventoryPlayerTwo[i] = lastDestroyedItem;
            break;
        }
    }
}

    ItemManager.Instance.ChangeModal();
    AudioManager.Instance.Play("collect");


    // SET ONGOING ANIMATION TO ZERO (IF THERE IS ONE) 
    AnimationManager.Instance.itemModal.GetComponent<Animator>().Rebind();
    AnimationManager.Instance.itemModal.GetComponent<Animator>().Update(0f);
   
    // ACTUAL ANIMATION      
    AnimationManager.Instance.AnimateItemModal();



    
}


public void DropOneItemPl1(){
    if(GameManager.Instance.GameState == GameState.Player1Turn){
        // IF PLAYER DOESNT HAVE ITEMS
        if(isFullPlayerOne[0] == false) {
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "You don't have items to drop";
        AnimationManager.Instance.AnimatePlayerText();
        }
        // DROP ITEM
        else {
        DropOneItem(Player1.Instance, InventoryManager.Instance.slotsPlayerOne, InventoryManager.Instance.isFullPlayerOne, InventoryManager.Instance.inventoryPlayerOne,InventoryManager.Instance.inventoryIsFullPlayerOne);
        Tile.Instance.ChangePlayerTurn();
        }
    }
    // ITS NOT YOUR TURN
    else {
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "It's not your turn";
        AnimationManager.Instance.AnimatePlayerText();
    }
}
public void DropOneItemPl2(){
    if(GameManager.Instance.GameState == GameState.Player2Turn){
        // IF PLAYER DOESNT HAVE ITEMS
        if(isFullPlayerTwo[0] == false) {
        MenuManager.Instance.RotateModalsToPlayer2();
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "You don't have items to drop";
        AnimationManager.Instance.AnimatePlayerText();
        }
        // DROP ITEM
        else{
        DropOneItem(Player2.Instance, InventoryManager.Instance.slotsPlayerTwo, InventoryManager.Instance.isFullPlayerTwo, InventoryManager.Instance.inventoryPlayerTwo,InventoryManager.Instance.inventoryIsFullPlayerTwo);
        Tile.Instance.ChangePlayerTurn();
        }
    } 
    // ITS NOT YOUR TURN
    else {
        MenuManager.Instance.RotateModalsToPlayer2();
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "It's not your turn";
        AnimationManager.Instance.AnimatePlayerText();
    }
}

public void SetNewItemSpawnTile(){
 // RANDOM TILE POS AROUNG THE PLAYER
     randomx = nineTiles[Random.Range(0,nineTiles.Count)];
     randomy = nineTiles[Random.Range(0,nineTiles.Count)];
     // IN CASE of 0 0
    if (randomx == 0 && randomy == 0){
        randomx = 1;
    }
}
public void DropOneItem(BasePlayer player, List <GameObject> slots, List <bool> isFull, List <string> inventory, bool inventoryIsFull){

randomx = 0;
randomy = 0;
    // SET SPAWNTILE
   SetNewItemSpawnTile();

    // ON AN EDGE? OR OCCUPIED? OR THE SPAWNPOINT OF A PLAYER?
    while(player.posx + randomx < 0 || player.posx + randomx >= GridManager.Instance._width || player.posy + randomy < 0 || player.posy + randomy >= GridManager.Instance._height ||
    GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit != null || GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy).OccupiedUnit2 != null ||
    GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy) ==  GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerOneSpawnTile, UnitManager.Instance.yPlayerOneSpawnTile) ||
    GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy) ==  GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerTwoSpawnTile, UnitManager.Instance.yPlayerTwoSpawnTile)
    ){
        Debug.Log( "redo itemspawntile");
        SetNewItemSpawnTile();
    }

    var spawnTileAroundPlayer = GridManager.Instance.GetSpawnTile(player.posx + randomx, player.posy + randomy);

    // Destroy the Slot Object
        for (int i = 0; i < slots.Count; i++)
        { 
            if(isFull[slots.Count-1 -i] == true){ 
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

        spawnedItem.transform.rotation = Quaternion.identity;
        spawnTileAroundPlayer.SetUnit(spawnedItem);
        lastDroppedItem = "";
    }
}
}
