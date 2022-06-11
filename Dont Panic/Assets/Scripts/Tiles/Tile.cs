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
    
    public BaseUnit OccupiedUnit, OccupiedUnit2;
    public string LastDoor;
    public bool Walkable => isWalkable && OccupiedUnit == null; // checks if tile is walkable and not occupied



    void Awake(){
        Instance = this;
    }
    public virtual void Init(int x, int y)  // will run on every tile, but each individula tile has the option to overwrite it
    {
      
    }


    public void OnMouseEnter(){  //works only on clicks now
    
                    multipleTouch.Instance.UpdateToken();
    } 

    void OnMouseExit(){ //works only on clicks now
    }

    void OnMouseDown(){       
        //InventoryManager.Instance.DropItemPl1();
        // when its occupied by a player or anything else
        if( OccupiedUnit != null ){ //when tile is occupied

            // FACTION PLAYER
            if(OccupiedUnit.Faction == Faction.Player){

                MenuManager.Instance.HideHelpers();
                // CLICKING ON ONESELF
                if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 1"  && Player1.Instance.deciding == true){
                    Debug.Log("pl1 clicked on himself");
                }  

                if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" && Player2.Instance.deciding == true){
                    Debug.Log("pl2 clicked on himself");
                   // InventoryManager.Instance.DropOneItem(Player2.Instance, InventoryManager.Instance.slotsPlayerTwo, InventoryManager.Instance.isFullPlayerTwo, InventoryManager.Instance.inventoryPlayerTwo,InventoryManager.Instance.inventoryIsFullPlayerTwo);                 
                   // ChangePlayerTurn();
                }  

                // CLICKING FOR MAKING TURN
                if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 1" ){
                    UnitManager.Instance.SetSelectedPlayer((Player1)OccupiedUnit);
                    Player1.Instance.deciding = true;
                    ShowWalkableTiles(Player1.Instance);
                    
                } else if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" ){
                    UnitManager.Instance.SetSelectedPlayer((Player2)OccupiedUnit);
                    Player2.Instance.deciding = true;
                    ShowWalkableTiles(Player2.Instance);
                    
                } 

                // THROWING PLAYER 1 
                 if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 1" && isWalkable == true){
                    Debug.Log("plyaer1 wird geworfen");
                    InventoryManager.Instance.DropOneItem(Player1.Instance, InventoryManager.Instance.slotsPlayerOne, InventoryManager.Instance.isFullPlayerOne, InventoryManager.Instance.inventoryPlayerOne,InventoryManager.Instance.inventoryIsFullPlayerOne);
                    ThrowPlayer(Player1.Instance);
                }

                // THROWING PLAYER 2
                else if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 2" && isWalkable == true ){
                    Debug.Log("plyaer2 wird geworfen");
                    InventoryManager.Instance.DropOneItem(Player2.Instance, InventoryManager.Instance.slotsPlayerTwo, InventoryManager.Instance.isFullPlayerTwo, InventoryManager.Instance.inventoryPlayerTwo,InventoryManager.Instance.inventoryIsFullPlayerTwo);                 
                    ThrowPlayer(Player2.Instance);
                }

            } 
            else { 
                if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true){ // if we have a selected player AND we click on another occupied unit 


                  
            // COLLISION ITEM
                            if(OccupiedUnit.Faction == Faction.Item){ // or occupiedUnit2


                                // CHECK IF PL1s INVENTORY IS FULL
                                if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" && InventoryManager.Instance.isFullPlayerOne[InventoryManager.Instance.slotsPlayerOne.Count -1] == true){
                                Debug.Log("inventory full pl1");
                                    InventoryManager.Instance.lastNotDestroyedItem = OccupiedUnit.name;
                                    Debug.Log(Tile.Instance.name);
                                    SetUnit(UnitManager.Instance.Item1);
                                    InventoryManager.Instance.itemUnderPlayer1 = true;
                                    InventoryManager.Instance.itemUnderPlayer1Tile = this;
                                    Debug.Log(InventoryManager.Instance.itemUnderPlayer1Tile);
                                    // das aktuelle Tile merken, nen boolean an
                                    // dann beim wegklicken boolean aus und item platzieren
                                InventoryManager.Instance.inventoryIsFullPlayerOne = true;
                                MenuManager.Instance.ShowInventoryIsFullText();
                                } 
                                // CHECK IF PL2s INVENTORY IS FULL
                                else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2"){ 
                                 if(InventoryManager.Instance.isFullPlayerTwo[InventoryManager.Instance.slotsPlayerTwo.Count -1] == true){
                                    // the item das eigentlich lastnotdestroyed heißt
                                    SetUnit(UnitManager.Instance.Item1);
                                    InventoryManager.Instance.itemUnderPlayer2 = true;
                                    InventoryManager.Instance.itemUnderPlayer2Tile = this;
                                    InventoryManager.Instance.lastNotDestroyedItem = OccupiedUnit.name;
                                    InventoryManager.Instance.inventoryIsFullPlayerTwo = true;
                                    MenuManager.Instance.ShowInventoryIsFullText();
                                    } 
                                }

                                // PLAYERS INVENTORY IS NOT FULL
                                if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player1Turn ||
                                InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player2Turn ){
                                DestroyUnit(); // muss hier oben sein
                                InventoryManager.Instance.ItemCollision();
                                } 
                                // PLAYERS INVENTORY IS FULL
                                if (InventoryManager.Instance.inventoryIsFullPlayerOne == true && GameManager.Instance.GameState == GameState.Player1Turn ||
                                InventoryManager.Instance.inventoryIsFullPlayerTwo == true && GameManager.Instance.GameState == GameState.Player2Turn ){
                                    ItemManager.Instance.oldImage.sprite = null;
                                    MenuManager.Instance.itemText.GetComponentInChildren<Text>().text = "inventory is full";
                                    ItemManager.Instance.ChangeModal();
                                    MenuManager.Instance.AnimateItemModal();
                                    
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
                    if (InventoryManager.Instance.itemUnderPlayer1 == false && GameManager.Instance.GameState == GameState.Player1Turn){
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    } else if (InventoryManager.Instance.itemUnderPlayer1 == true && GameManager.Instance.GameState == GameState.Player1Turn){
                        OccupiedUnit2 = OccupiedUnit;
                        SetUnit(UnitManager.Instance.SelectedPlayer);
                        InventoryManager.Instance.ItemTransferred1 = true;
                    }

                    if (InventoryManager.Instance.itemUnderPlayer2 == false && GameManager.Instance.GameState == GameState.Player2Turn){
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    } else if (InventoryManager.Instance.itemUnderPlayer1 == true && GameManager.Instance.GameState == GameState.Player2Turn) {
                        OccupiedUnit2 = OccupiedUnit;
                        SetUnit(UnitManager.Instance.SelectedPlayer);
                        InventoryManager.Instance.ItemTransferred2 = true;
                    }

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
            if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true && TileName == "Floor"){
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


                // ITEM WAS UNDER A PLAYER
                if(InventoryManager.Instance.itemUnderPlayer1 == true && GameManager.Instance.GameState == GameState.Player1Turn){
                    InventoryManager.Instance.itemUnderPlayer1Tile.SetUnit(UnitManager.Instance.Item1);
                    OccupiedUnit2 = null;
                    InventoryManager.Instance.itemUnderPlayer1 = false;
                    InventoryManager.Instance.itemUnderPlayer1Tile.OccupiedUnit2 =null;
                } else if (InventoryManager.Instance.itemUnderPlayer2 == true && GameManager.Instance.GameState == GameState.Player2Turn){
                    InventoryManager.Instance.itemUnderPlayer2Tile.SetUnit(UnitManager.Instance.Item1);
                    OccupiedUnit2 = null;
                    InventoryManager.Instance.itemUnderPlayer2 = false;
                    InventoryManager.Instance.itemUnderPlayer2Tile.OccupiedUnit2 =null;
                } 


                ChangePlayerTurn();
            } else {
                Debug.Log("youre not on the highlight");
                AnimationManager.Instance.AnimateHighlightTiles();
            }
        }
    } 
    
    
int playerSpawnTileX;
int playerSpawnTileY;
    public void ThrowPlayer(BasePlayer playerToBeThrown){
        AudioManager.Instance.Play("throw");
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
            MenuManager.Instance.AnimateItemModal();
            } else if (InventoryManager.Instance.inventoryIsFullPlayerOne == true && GameManager.Instance.GameState == GameState.Player2Turn ||
              InventoryManager.Instance.inventoryIsFullPlayerTwo == true && GameManager.Instance.GameState == GameState.Player1Turn ){
              // if inventory is full
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
        AudioManager.Instance.Play("throw");
                        GridManager.Instance.GetSpawnTile(UnitManager.Instance.xPlayerOneSpawnTile, UnitManager.Instance.yPlayerOneSpawnTile).SetUnit(Player1.Instance);
                        Player1.Instance.posx = UnitManager.Instance.xPlayerOneSpawnTile;
                        Player1.Instance.posy = UnitManager.Instance.yPlayerOneSpawnTile;
    }

    public void ThrowPlayer2ByPatrol(){
        AudioManager.Instance.Play("throw");
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
        Player1.Instance.highlight.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Player2Turn);
        } else if(GameManager.Instance.GameState == GameState.Player2Turn){
        UpdatePosition(Player2.Instance);
        HideWalkableTiles();
        Player2.Instance.highlight.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Player1Turn);
        }
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


    public void IsTokenOnSelectedPlayer(){
        GameObject playerone = GameObject.Find("playerone(Clone)");
        GameObject playertwo = GameObject.Find("playertwo(Clone)");
        Debug.Log(playerone.GetComponent<Player1>().posx + "" + playerone.GetComponent<Player1>().posy);


       // wenn t, t1, t2 zumindest einer von denen auf dem Tile is


    }
}
