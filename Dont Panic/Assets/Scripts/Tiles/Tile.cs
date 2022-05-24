using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    public static Tile Instance;
    [SerializeField] protected SpriteRenderer _renderer;  // affectively private, but derived tiles can access it
    [SerializeField]private bool isWalkable;
    [SerializeField] public GameObject highlight;
    private bool playerOnDoor;
    
    public BaseUnit OccupiedUnit;

    public string LastDoor;
    public bool Walkable => isWalkable && OccupiedUnit == null; // checks if tile is walkable and not occupied



    void Awake(){
        Instance = this;
    }
    public virtual void Init(int x, int y)  // will run on every tile, but each individula tile has the option to overwrite it
    {
      
    }


    public void OnMouseEnter(){  //works only on clicks now
    } 

    void OnMouseExit(){ //works only on clicks now
    }

    void OnMouseDown(){
        //InventoryManager.Instance.DropItemPl1();
        // when its occupied by a player or anything else
        if( OccupiedUnit != null ){ //when tile is occupied

            if(OccupiedUnit.Faction == Faction.Player){
                // CLICKING FOR MAKING TURN
                if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 1" ){
                    UnitManager.Instance.SetSelectedPlayer((Player1)OccupiedUnit);
                    //if(MultipleTouch.Instance.objectOneRecognized == true){
                    Player1.Instance.highlight.SetActive(true);
                    ShowWalkableTiles(Player1.Instance);  
                    Player1.Instance.deciding = true;
                } else if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" ){
                    UnitManager.Instance.SetSelectedPlayer((Player2)OccupiedUnit);
                    //if(MultipleTouch.Instance.objectTwoRecognized == true){
                    Player2.Instance.highlight.SetActive(true);
                    ShowWalkableTiles(Player2.Instance);
                    Player2.Instance.deciding = true;
                    
                } 
                
                // THROWING PLAYER 1 
                 if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 1" && isWalkable == true){
                    Debug.Log("plyaer1 wird geworfen");
                    ThrowPlayer(Player1.Instance);
                    InventoryManager.Instance.DropOneItem(Player1.Instance, InventoryManager.Instance.slotsPlayerOne, InventoryManager.Instance.isFullPlayerOne, InventoryManager.Instance.inventoryPlayerOne,InventoryManager.Instance.inventoryIsFullPlayerOne);
                    
                }

                // THROWING PLAYER 2
                //&& isWalkable == true 
                else if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 2" && isWalkable == true ){
                    Debug.Log("plyaer2 wird geworfen");
                     ThrowPlayer(Player2.Instance);
                    InventoryManager.Instance.DropOneItem(Player2.Instance, InventoryManager.Instance.slotsPlayerTwo, InventoryManager.Instance.isFullPlayerTwo, InventoryManager.Instance.inventoryPlayerTwo,InventoryManager.Instance.inventoryIsFullPlayerTwo);                 
                }

            } 
            else { 
                if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true){ // if we have a selected player AND we click on another occupied unit 
                    
            // COLLISION ITEM
                            if(OccupiedUnit.Faction == Faction.Item){ 
                                // just destroy it when the inventory of the player isnt completely full
                                if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player1Turn ||
                                InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player2Turn ){
                                DestroyUnit();
                                // zsm fassen
                                InventoryManager.Instance.ItemCollision();
                                ItemManager.Instance.ChangeModal();
                                MenuManager.Instance.ShowItemModal();
                                MenuManager.Instance.AnimateItemModal();
                                } else if (InventoryManager.Instance.inventoryIsFullPlayerOne == true && GameManager.Instance.GameState == GameState.Player1Turn ||
                                InventoryManager.Instance.inventoryIsFullPlayerTwo == true && GameManager.Instance.GameState == GameState.Player2Turn ){
                                    // if inventory is full
                                    MenuManager.Instance.ShowInventoryIsFullText();
                                }
                            }    

                        
            // COLLISION DOOR
                        if(OccupiedUnit.Faction == Faction.Door){
                            DoorManager.Instance.lastVisitedDoor = OccupiedUnit.UnitName;
                            LastDoor = OccupiedUnit.UnitName;
                            DoorManager.Instance.DoorCollision();
                            playerOnDoor = true;
                            MenuManager.Instance.AnimatePlayerText();
                            MenuManager.Instance.AnimateDoorModal();
                        }
                        
            // COLLISION PATROL       
               // if patrol is on tile, mark the tiles as infiziert, sagen wir vorerst mal walkable     
                        //OccupiedUnit.Faction = Faction.Patrol;
                        //randomSpawnTile = GridManager.Instance.GetDoorSpawnTile(doorUpValue2, GridManager.Instance._height -1);
                        

                    //deselect selected Unit
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);
                    ChangePlayerTurn();
                    // PANTRY FEATURE CHANGE TURNS AGAIN
                    if(DoorManager.Instance.pantryFeatureP1 == true){
                        ChangePlayerTurn();
                        DoorManager.Instance.pantryFeatureP1 = false;
                    } else if(DoorManager.Instance.pantryFeatureP2 == true ){
                        ChangePlayerTurn();
                        DoorManager.Instance.pantryFeatureP2 = false;
                    }
                }

            }

    	    
        }
        else {
            // already got a selected Unit
            if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true){
                    
                
                 //deselect selected Unit
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                     UnitManager.Instance.SetSelectedPlayer(null);
                 


                 if (playerOnDoor ==true){
                    Debug.Log("player on door the second time");
                    DoorManager.Instance.lastVisitedDoor = LastDoor;
                    DoorManager.Instance.DoorCollision();
                    MenuManager.Instance.AnimatePlayerText();
                    MenuManager.Instance.AnimateDoorModal();
                }

                ChangePlayerTurn();
            }
        }


    } 
    
int playerSpawnTileX;
int playerSpawnTileY;
    public void ThrowPlayer(BasePlayer playerToBeThrown){
        int i = Mathf.RoundToInt(OccupiedUnit.transform.position.x);
        int j = Mathf.RoundToInt(OccupiedUnit.transform.position.y);
        if(UnitManager.Instance.SelectedPlayer!= null){
            GridManager.Instance.GetSpawnTile(i,j).SetUnit(playerToBeThrown);

        // PLAYER ONE
        if (playerToBeThrown = Player1.Instance){
            GameObject playertwo = GameObject.Find("playertwo(Clone)");
            playertwo.transform.position = new Vector3(i,j,0);
            playerSpawnTileX = UnitManager.Instance.xPlayerOneSpawnTile;
            playerSpawnTileY = UnitManager.Instance.yPlayerOneSpawnTile;
        }
        // PLAYER TWO
        if (playerToBeThrown = Player2.Instance){
            GameObject playerone = GameObject.Find("playerone(Clone)");
            playerone.transform.position = new Vector3(i,j,0);
            playerSpawnTileX = UnitManager.Instance.xPlayerTwoSpawnTile;
            playerSpawnTileY = UnitManager.Instance.yPlayerTwoSpawnTile;
        }
            
                        
        // IF PLAYER GETS ON AN ITEM
        if (GridManager.Instance.GetSpawnTile(playerSpawnTileX, playerSpawnTileY).OccupiedUnit != null){
            if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player2Turn ||
                InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player1Turn ){

                    // PLAYER ONE
                    if( playerToBeThrown == Player1.Instance){
                    //GridManager.Instance.GetSpawnTile(playerSpawnTileX, playerSpawnTileY).DestroyUnit();     
                    MenuManager.Instance.RotateModalsToPlayer1();
                            for (int k = 0; k < InventoryManager.Instance.slotsPlayerOne.Count; k++)
                            {
                                if(InventoryManager.Instance.isFullPlayerOne[k] == false ){ // item can be added to inventory
                                    // parented to slots[k]
                                    Instantiate(InventoryManager.Instance.inventoryPoint, InventoryManager.Instance.slotsPlayerOne[k].transform, false);
                                    InventoryManager.Instance.isFullPlayerOne[k] = true;
                                    InventoryManager.Instance.inventoryPlayerOne[k] = InventoryManager.Instance.lastDestroyedItem;
                                    break;
                                }
                            }

                    if(InventoryManager.Instance.isFullPlayerOne[InventoryManager.Instance.slotsPlayerOne.Count -1] == true){
                        Debug.Log("inventory full pl1");
                        InventoryManager.Instance.inventoryIsFullPlayerOne = true;
                    } 
                    }

                    // PLAYER TWO
                    if (playerToBeThrown = Player2.Instance){
                        // could be the player itself not the item, not quite sure
                   // GridManager.Instance.GetSpawnTile(playerSpawnTileX, playerSpawnTileY).DestroyUnit();     
                    MenuManager.Instance.RotateModalsToPlayer2();
                            for (int k = 0; k < InventoryManager.Instance.slotsPlayerTwo.Count; k++)
                            {
                                if(InventoryManager.Instance.isFullPlayerTwo[k] == false ){ // item can be added to inventory
                                    // parented to slots[k]
                                    Instantiate(InventoryManager.Instance.inventoryPoint, InventoryManager.Instance.slotsPlayerTwo[k].transform, false);
                                    InventoryManager.Instance.isFullPlayerTwo[k] = true;
                                    InventoryManager.Instance.inventoryPlayerTwo[k] = InventoryManager.Instance.lastDestroyedItem;
                                    break;
                                }
                            }

                    if(InventoryManager.Instance.isFullPlayerTwo[InventoryManager.Instance.slotsPlayerTwo.Count -1] == true){
                        Debug.Log("inventory full pl1");
                        InventoryManager.Instance.inventoryIsFullPlayerTwo = true;
                    }
                    }
            ItemManager.Instance.ChangeModal();
            MenuManager.Instance.ShowItemModal();
            MenuManager.Instance.AnimateItemModal();
            } else if (InventoryManager.Instance.inventoryIsFullPlayerOne == true && GameManager.Instance.GameState == GameState.Player2Turn ||
              InventoryManager.Instance.inventoryIsFullPlayerTwo == true && GameManager.Instance.GameState == GameState.Player1Turn ){
              // if inventory is full
            MenuManager.Instance.ShowInventoryIsFullText();
             }
            Debug.Log(" jetzt isser auf ein item beim schmeißen");
                            
        }
        if(playerToBeThrown = Player1.Instance){
                            GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerOneSpawnTile, UnitManager.Instance.yPlayerOneSpawnTile).SetUnit(playerToBeThrown);
                            GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerOneSpawnTile, UnitManager.Instance.yPlayerOneSpawnTile).OccupiedUnit = playerToBeThrown;
                            playerToBeThrown.posx = UnitManager.Instance.xPlayerOneSpawnTile;
                            playerToBeThrown.posy = UnitManager.Instance.yPlayerOneSpawnTile;
        }
                        if(playerToBeThrown = Player2.Instance){
                            GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerTwoSpawnTile, UnitManager.Instance.yPlayerTwoSpawnTile).SetUnit(playerToBeThrown);
                            GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerTwoSpawnTile, UnitManager.Instance.yPlayerTwoSpawnTile).OccupiedUnit = playerToBeThrown;
                            playerToBeThrown.posx = UnitManager.Instance.xPlayerTwoSpawnTile;
                            playerToBeThrown.posy = UnitManager.Instance.yPlayerTwoSpawnTile;
                            // is das nicht einfach set unit?
                        }
                      SetUnit(UnitManager.Instance.SelectedPlayer);
                      UnitManager.Instance.SetSelectedPlayer(null);

                        // dehighlight all
                        
                        ChangePlayerTurn();
                        
                        }

    }

    public void ThrowPlayer1ByPatrol(){
                        GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerOneSpawnTile, UnitManager.Instance.yPlayerOneSpawnTile).SetUnit(Player1.Instance);
                        Player1.Instance.posx = UnitManager.Instance.xPlayerOneSpawnTile;
                        Player1.Instance.posy = UnitManager.Instance.yPlayerOneSpawnTile;
    }

    public void ThrowPlayer2ByPatrol(){
                        GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerTwoSpawnTile, UnitManager.Instance.yPlayerTwoSpawnTile).SetUnit(Player2.Instance);
                        Player2.Instance.posx = UnitManager.Instance.xPlayerTwoSpawnTile;
                        Player2.Instance.posy = UnitManager.Instance.yPlayerTwoSpawnTile;

                        // here auch noch wenn item daheim lieggt
                        // oder ein anderer Spieler
    }

    public void SetUnit(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position =transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void DestroyUnit(){
        Destroy(OccupiedUnit.gameObject);
        InventoryManager.Instance.lastDestroyedItem = OccupiedUnit.UnitName;
    }

    public void ChangePlayerTurn(){
    if(GameManager.Instance.GameState == GameState.Player1Turn){
        UpdatePosition(Player1.Instance);
        HideWalkableTiles();
        GameManager.Instance.ChangeState(GameState.Player2Turn);
        } else if(GameManager.Instance.GameState == GameState.Player2Turn){
        UpdatePosition(Player2.Instance);
        HideWalkableTiles();
        GameManager.Instance.ChangeState(GameState.Player1Turn);
        }
        // DEHIGHLIGHT
        Player1.Instance.highlight.SetActive(false);
        Player2.Instance.highlight.SetActive(false);
        Player1.Instance.deciding = false;
        Player2.Instance.deciding = false;

    }

    public void UpdatePosition(BasePlayer player){
    char cx = player.OccupiedTile.name[5];
    char cy = player.OccupiedTile.name[7];
    player.posx = cx -48;
    player.posy = cy -48;
    //Debug.Log("player stands on Tile " + player.posx + " " + player.posy);
    }

    public void ShowWalkableTiles(BasePlayer player){
        HideWalkableTiles();
            for (int y = 1; y < player.walkingDistance; y++)
            {
                // DOWN
                if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_down" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_down" ){
                    
                    // DOWN
                    if(player.posy -y >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - y )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - y )].isWalkable = true;
                    }
                    //LEFT
                    if(player.posx -1 >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx - 1, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx - 1, player.posy)].isWalkable = true;
                    }
                     // RIGHT
                    if(player.posx + y < GridManager.Instance._width){
                    GridManager.Instance.tiles[new Vector2(player.posx + 1, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx + 1, player.posy)].isWalkable = true;
                    // JUST BECAUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUSE
                    // UP
                    if(player.posy + y < GridManager.Instance._height){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + y )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + y )].isWalkable = true;
                    }
                    }
                 } 
                 
                 //UP
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_up" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_up"){
                    // UP
                    if(player.posy + y < GridManager.Instance._height){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + y )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + y )].isWalkable = true;
                    }
                    //LEFT
                    if(player.posx -1 >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx - 1, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx - 1, player.posy)].isWalkable = true;
                    }
                     // RIGHT
                    if(player.posx + y < GridManager.Instance._width){
                    GridManager.Instance.tiles[new Vector2(player.posx + 1, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx + 1, player.posy)].isWalkable = true;
                    }
                    
                 } 
                 
                 //LEFT
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_left" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_left"){
                    // LEFT
                    if(player.posx -y >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy)].isWalkable = true;
                    }
                    // UP
                    if(player.posy + y < GridManager.Instance._height){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + 1 )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + 1 )].isWalkable = true;
                    }
                     // DOWN
                    if(player.posy -y >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - 1 )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - 1 )].isWalkable = true;
                    }
                 } 
                 
                 //RIGHT
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_right" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_right"){
                    // RIGHT
                    if(player.posx + y < GridManager.Instance._width){
                    GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy)].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy)].isWalkable = true;
                    }
                    // UP
                    if(player.posy + y < GridManager.Instance._height){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + 1 )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + 1 )].isWalkable = true;
                    }
                    // DOWN
                    if(player.posy -y >= 0){
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - 1 )].highlight.SetActive(true);
                    GridManager.Instance.tiles[new Vector2(player.posx, player.posy - 1 )].isWalkable = true;
                    }
                 }                 
            }
    // walking diagonal
            /* for (int y = 1; y < player.walkingDistance - 1; y++)
            {
                if(player.posx - y >= 0 && player.posy -y >= 0){
                GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy - y )].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy - y )].isWalkable = true;
                }
                if(player.posx + y < GridManager.Instance._width && player.posy + y < GridManager.Instance._height ){
                GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy + y)].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy + y)].isWalkable = true;
                }
                
                if(player.posx -y >= 0 && player.posy + y < GridManager.Instance._height){
                GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy + y )].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(player.posx - y, player.posy + y )].isWalkable = true;
                }
                if(player.posx + y < GridManager.Instance._width && player.posy -y >= 0){
                GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy - y)].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(player.posx + y, player.posy - y)].isWalkable = true;
                }
            } */
        // if statements for not reaching out of the table
    }

    public void HideWalkableTiles(){
// make sure everything is not walkable at first
        for (int x = 0; x < GridManager.Instance._width; x++)
        {
            for (int y = 0; y < GridManager.Instance._height; y++)
            {
                GridManager.Instance.tiles[new Vector2(x,y)].isWalkable = false;
                GridManager.Instance.tiles[new Vector2(x, y )].highlight.SetActive(false);
                
            }
        }
    }
}
