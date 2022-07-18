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

    private bool playerOnDoorSecondTime;
    
    public BaseUnit OccupiedUnit, OccupiedUnit2;
    public bool Walkable => isWalkable && OccupiedUnit == null; // checks if tile is walkable and not occupied

    public Color recentColor = Color.black;
    void Awake(){
        Instance = this;
    }

    void Update(){
    }
    public virtual void Init(int x, int y)  // will run on every tile, but each individula tile has the option to overwrite it
    {
      
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
                    multipleTouch.Instance.standsInPlace = true;
                }  

                if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" && Player2.Instance.deciding == true){
                    multipleTouch.Instance.standsInPlace = true;
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
                    InventoryManager.Instance.DropOneItem(Player1.Instance, InventoryManager.Instance.slotsPlayerOne, InventoryManager.Instance.isFullPlayerOne, InventoryManager.Instance.inventoryPlayerOne,InventoryManager.Instance.inventoryIsFullPlayerOne);
                    ThrowPlayer(1);
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);
                    ChangePlayerTurn();
                }

                // THROWING PLAYER 2
                else if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 2" && isWalkable == true ){
                    Debug.Log("plyaer2 wird geworfen");
                    InventoryManager.Instance.DropOneItem(Player2.Instance, InventoryManager.Instance.slotsPlayerTwo, InventoryManager.Instance.isFullPlayerTwo, InventoryManager.Instance.inventoryPlayerTwo,InventoryManager.Instance.inventoryIsFullPlayerTwo);                 
                    ThrowPlayer(2);
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);
                    ChangePlayerTurn();
                }

            } 
            else { 
                if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true){ // if we have a selected player AND we click on another occupied unit 
                // WALK ON ANOTHER OCCUPIED UNIT
                    
                    multipleTouch.Instance.standsInPlace = false;
                    // COLLISION ITEM
                    if(OccupiedUnit2 != null && OccupiedUnit2.Faction == Faction.Item){ 
                        CollisionWithItemOnOcc2();
                    }   

                    if(OccupiedUnit.Faction == Faction.Item){ 
                        CollisionWithItem(OccupiedUnit, OccupiedUnit2);
                        // hier is occ1 noch item
                    Debug.Log(OccupiedUnit + " " + OccupiedUnit2);
                    Destroy(OccupiedUnit.gameObject);
                    } 

                    
                    // COLLISION DOOR
                    // wird nur beim ersten mal ausgeführt
                    if(OccupiedUnit != null && OccupiedUnit.Faction == Faction.Door){
                        OccupiedUnit2 = OccupiedUnit;
                        DoorManager.Instance.lastVisitedDoor = OccupiedUnit.UnitName;
                        DoorManager.Instance.DoorCollision();
                    }

                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);
                    ChangePlayerTurn();

                    // hier dann nicht mehr
                    // PANTRY FEATURE CHANGE TURNS AGAIN
                    if(DoorManager.Instance.pantryFeatureP1 == true || DoorManager.Instance.pantryFeatureP2 == true ){
                        ChangePlayerTurn();
                        DoorManager.Instance.pantryFeatureP1 = false;
                        DoorManager.Instance.pantryFeatureP2 = false;
                    } 
                
                AudioManager.Instance.Play("set");
                }

            } 
        }
        else {
            
            multipleTouch.Instance.standsInPlace = false;
            // NOT WALKABLE TILE
            if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true && TileName == "Hole"){
                AudioManager.Instance.Play("error");
            }
            // WALK ONLY
            if(UnitManager.Instance.SelectedPlayer != null && isWalkable == true && TileName == "Floor"){
            AudioManager.Instance.Play("set");
                 //deselect selected Unit
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);

                // COLLISION DOOR SECOND TIME
                if ( OccupiedUnit2 != null && OccupiedUnit2.Faction == Faction.Door){
                    DoorManager.Instance.lastVisitedDoor = OccupiedUnit2.UnitName;
                    DoorManager.Instance.DoorCollision();
                }
                
                // IF ITEM WAS UNDER A PLAYER
                PutItemBackInPlace(); 

                ChangePlayerTurn();
            } else {
                // YOURE NOT ON THE HIGHLIGHT
                
                multipleTouch.Instance.standsInPlace = false;
                AnimationManager.Instance.AnimateHighlightTiles();
            }
        }
    } 
    
    public void PutItemBackInPlace(){
        if(InventoryManager.Instance.itemUnderPlayer1 == true && GameManager.Instance.GameState == GameState.Player1Turn){
            InventoryManager.Instance.itemUnderPlayer1Tile.SetUnit(MakeStringIntoItem(InventoryManager.Instance.lastNotDestroyedItem));
                    InventoryManager.Instance.itemUnderPlayer1 = false;
                    InventoryManager.Instance.itemUnderPlayer1Tile.OccupiedUnit2 =null;
                    InventoryManager.Instance.itemUnderPlayer1Tile = null;
        } else if (InventoryManager.Instance.itemUnderPlayer2 == true && GameManager.Instance.GameState == GameState.Player2Turn){
                    InventoryManager.Instance.itemUnderPlayer2Tile.SetUnit(MakeStringIntoItem(InventoryManager.Instance.lastNotDestroyedItem));
                    InventoryManager.Instance.itemUnderPlayer2 = false;
                    InventoryManager.Instance.itemUnderPlayer2Tile.OccupiedUnit2 =null;
                    InventoryManager.Instance.itemUnderPlayer2Tile = null;
        } 
    }

    public void RotateModals(){
            if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" ){
            MenuManager.Instance.RotateModalsToPlayer1();
            } else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2"){
            MenuManager.Instance.RotateModalsToPlayer2();
            }
    }
   
   public void CollisionWithItem(BaseUnit occ1, BaseUnit occ2){
    RotateModals();
    if (InventoryManager.Instance.lastDestroyedItem == InventoryManager.Instance.lastNotDestroyedItem){
        Debug.Log("last destroyed and last not destroyed are the same, is there any bug?");
    }

    // PL1s INVENTORY IS FULL AND NOT COLLECTING ITEM ON OCCUNIT1
    if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" && InventoryManager.Instance.isFullPlayerOne[InventoryManager.Instance.slotsPlayerOne.Count -1] == true){
        InventoryManager.Instance.lastNotDestroyedItem = occ1.UnitName;
        InventoryManager.Instance.itemUnderPlayer1 = true;
        InventoryManager.Instance.itemUnderPlayer1Tile = this;
        InventoryManager.Instance.inventoryIsFullPlayerOne = true;
        occ2 = occ1;
        SetUnit2(occ2);
        Debug.Log(occ1 + " " + occ2);
    } 
    // PL2s INVENTORY IS FULL AND NOT COLLECTING ITEM ON OCCUNIT1
    else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2" && InventoryManager.Instance.isFullPlayerTwo[InventoryManager.Instance.slotsPlayerTwo.Count -1] == true){ 
        InventoryManager.Instance.lastNotDestroyedItem = OccupiedUnit.UnitName;
        InventoryManager.Instance.itemUnderPlayer2 = true;
        InventoryManager.Instance.itemUnderPlayer2Tile = this;
        InventoryManager.Instance.inventoryIsFullPlayerTwo = true;
        occ2 = occ1;
        SetUnit2(occ2);
        Debug.Log(occ1 + " " + occ2);
    }

    // PLAYERS INVENTORY IS NOT FULL
    if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player1Turn ||
    InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player2Turn){
    occ1.OccupiedTile = null;
    DestroyUnit(); // muss hier oben sein 
    //Destroy(occ1.gameObject);

    InventoryManager.Instance.AddItemToInventory();
    AnimationManager.Instance.AnimateInventoryPoint();
    } 

    // PLAYERS INVENTORY IS FULL TEXT
    if (InventoryManager.Instance.inventoryIsFullPlayerOne == true ||
    InventoryManager.Instance.inventoryIsFullPlayerTwo == true ){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "Your Inventory is Full";
        AnimationManager.Instance.AnimatePlayerText();

        // RESET EVERYTHING
        InventoryManager.Instance.inventoryIsFullPlayerOne = false;
        InventoryManager.Instance.inventoryIsFullPlayerTwo = false; 
    }
   }

// setzt schon ein wenn noch nichts auf occ2 drauf ist
   public void CollisionWithItemOnOcc2(){
    if (InventoryManager.Instance.lastDestroyedItem == InventoryManager.Instance.lastNotDestroyedItem){
        Debug.Log("last destroyed and last not destroyed are the same, is there any bug?");
    }

    Debug.Log("Collision with item with occ2");
    RotateModals();
     // PL1s INVENTORY IS FULL AND NOT COLLECTING ITEM ON OCCUNIT2
    if(UnitManager.Instance.SelectedPlayer.UnitName == "player 1" && InventoryManager.Instance.isFullPlayerOne[InventoryManager.Instance.slotsPlayerOne.Count -1] == true){
        InventoryManager.Instance.lastNotDestroyedItem = OccupiedUnit2.UnitName;
        InventoryManager.Instance.itemUnderPlayer1 = true;
        InventoryManager.Instance.itemUnderPlayer1Tile = this;
        InventoryManager.Instance.inventoryIsFullPlayerOne = true;
    } 
    // PL2s INVENTORY IS FULL AND NOT COLLECTING ITEM ON OCCUNIT1
    else if(UnitManager.Instance.SelectedPlayer.UnitName == "player 2" && InventoryManager.Instance.isFullPlayerTwo[InventoryManager.Instance.slotsPlayerTwo.Count -1] == true){ 
        InventoryManager.Instance.lastNotDestroyedItem = OccupiedUnit2.UnitName;
        InventoryManager.Instance.itemUnderPlayer2 = true;
        InventoryManager.Instance.itemUnderPlayer2Tile = this;
        InventoryManager.Instance.inventoryIsFullPlayerTwo = true;
    }

    // PLAYERS INVENTORY IS NOT FULL
    if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player1Turn ||
    InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player2Turn){
    Debug.Log("destroy unit2 ");
    DestroyUnit2(); // muss hier oben sein  oder UNIT2
    InventoryManager.Instance.AddItemToInventory();
    AnimationManager.Instance.AnimateInventoryPoint();
    } 
// PLAYERS INVENTORY IS FULL TEXT
    if (InventoryManager.Instance.inventoryIsFullPlayerOne == true ||
    InventoryManager.Instance.inventoryIsFullPlayerTwo == true ){
        MenuManager.Instance.PlayerText.GetComponentInChildren<Text>().text = "Your Inventory is Full";

    // SET ONGOING ANIMATION TO ZERO (IF THERE IS ONE) 
    AnimationManager.Instance.TextForPlayerModal.GetComponent<Animator>().Rebind();
    AnimationManager.Instance.TextForPlayerModal.GetComponent<Animator>().Update(0f);

        AnimationManager.Instance.AnimatePlayerText();

        // RESET EVERYTHING
        InventoryManager.Instance.inventoryIsFullPlayerOne = false;
        InventoryManager.Instance.inventoryIsFullPlayerTwo = false; 
    }
}
    public void ThrowPlayer(int i){
    // AUDIO
    AudioManager.Instance.Play("throw");
    // RESET 
    int xSpawnTile = 0;
    int ySpawnTile = 0;
 
        if (i == 1){
            xSpawnTile = UnitManager.Instance.xPlayerOneSpawnTile;
            ySpawnTile = UnitManager.Instance.yPlayerOneSpawnTile;
        }
        if (i == 2){
            xSpawnTile = UnitManager.Instance.xPlayerTwoSpawnTile;
            ySpawnTile = UnitManager.Instance.yPlayerTwoSpawnTile;
        }

        // WHEN THIS TILE IS THE PLAYER TO BE THROWN S SPAWNING TILE
        if( this == GridManager.Instance.GetSpawnTile(xSpawnTile, ySpawnTile)){
            if (i == 1){ // SWITCH
                xSpawnTile = UnitManager.Instance.xPlayerTwoSpawnTile;
                ySpawnTile = UnitManager.Instance.yPlayerTwoSpawnTile;
            }

            if (i == 2){ // SWITCH
                xSpawnTile = UnitManager.Instance.xPlayerOneSpawnTile;
                ySpawnTile = UnitManager.Instance.yPlayerOneSpawnTile;
            }
        }

        // WHEN PLAYER TO BE THROWN IS OVER A DOOR
        if (OccupiedUnit2 != null && OccupiedUnit2.Faction == Faction.Door){
                        DoorManager.Instance.lastVisitedDoor = OccupiedUnit2.UnitName;
                        DoorManager.Instance.DoorCollision();
                }

        // WHEN PLAYER TO BE THROWN IS OVER AN ITEM
        if (OccupiedUnit2 != null && OccupiedUnit2.Faction == Faction.Item){
             //CollisionWithItem(OccupiedUnit2, OccupiedUnit);
             Debug.Log("there is an item underneath");
        }
        // SET PLAYER ONE
        if ( i == 1){
            GridManager.Instance.GetSpawnTile(xSpawnTile, ySpawnTile).SetUnit(Player1.Instance);
            Player1.Instance.posx = xSpawnTile;
            Player1.Instance.posy = ySpawnTile;

        // SET PLAYER TWO
        } else if(i == 2){
            GridManager.Instance.GetSpawnTile(xSpawnTile, ySpawnTile).SetUnit(Player2.Instance);
            Player2.Instance.posx = xSpawnTile;
            Player2.Instance.posy = ySpawnTile;
        }
        
        // WHEN THE SPAWNING TILE IS OCCUPIED
        // HOLE
        if (GridManager.Instance.GetSpawnTile(xSpawnTile, ySpawnTile).TileName == "Hole"){
                Debug.Log("player landed on a hole");
            }
        // ITEM
         if (GridManager.Instance.GetSpawnTile(xSpawnTile, ySpawnTile).OccupiedUnit.Faction == Faction.Item){
                Debug.Log("player landed on an item");
             }
    }
    public void SetUnit(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position =transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void SetUnit2(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit2 = null;
        unit.transform.position =transform.position;
        OccupiedUnit2 = unit;
        unit.OccupiedTile = this;
    }

    public void DestroyUnit(){
        Debug.Log("destroy Unit " + OccupiedUnit);
        Destroy(OccupiedUnit.gameObject);
        InventoryManager.Instance.lastDestroyedItem = OccupiedUnit.UnitName;
    }

    public void DestroyUnit2(){
        // destroyed das nicht weil item6 clone dasteht und nicht item6 clone (item)
        Destroy(OccupiedUnit2.gameObject);
        InventoryManager.Instance.lastDestroyedItem = OccupiedUnit2.UnitName;
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
    }
    public void WalkLeft(BasePlayer player, int intLeft, Color recentColor){
        if(player.posx - intLeft >= 0){
        GridManager.Instance.tiles[new Vector2(player.posx - intLeft, player.posy)].highlight.GetComponent<SpriteRenderer>().color = recentColor;
        GridManager.Instance.tiles[new Vector2(player.posx - intLeft, player.posy)].highlight.SetActive(true);
        GridManager.Instance.tiles[new Vector2(player.posx - intLeft, player.posy)].isWalkable = true;
        }
    }

    public void WalkRight(BasePlayer player, int intRight, Color recentColor){
        if(player.posx + intRight < GridManager.Instance._width){
        GridManager.Instance.tiles[new Vector2(player.posx + intRight, player.posy)].highlight.GetComponent<SpriteRenderer>().color = recentColor;  
        GridManager.Instance.tiles[new Vector2(player.posx + intRight, player.posy)].highlight.SetActive(true);
        GridManager.Instance.tiles[new Vector2(player.posx + intRight, player.posy)].isWalkable = true;
        }
    }
    public void WalkUp(BasePlayer player, int intUp, Color recentColor){
    if(player.posy + intUp < GridManager.Instance._height){
    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + intUp )].highlight.GetComponent<SpriteRenderer>().color = recentColor;
    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + intUp )].highlight.SetActive(true);
    GridManager.Instance.tiles[new Vector2(player.posx, player.posy + intUp )].isWalkable = true;
    }
    }
    public void WalkDown(BasePlayer player,int intDown, Color recentColor){
        if(player.posy - intDown>= 0){
        GridManager.Instance.tiles[new Vector2(player.posx, player.posy - intDown )].highlight.GetComponent<SpriteRenderer>().color = recentColor;
        GridManager.Instance.tiles[new Vector2(player.posx, player.posy - intDown )].highlight.SetActive(true);
        GridManager.Instance.tiles[new Vector2(player.posx, player.posy - intDown )].isWalkable = true;
        }
    }

    public void ShowWalkableTiles(BasePlayer player){
       if(Player1.Instance.deciding){
        recentColor =  GridManager.Instance.colorPlayer1;
       } else if(Player2.Instance.deciding){
        recentColor =  GridManager.Instance.colorPlayer2;
       }

        HideWalkableTiles();
       
            for (int y = 1; y < player.walkingDistance; y++)
            {
                // DOWN
                if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_down" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_down" ){
                    WalkLeft(player, 2, recentColor);
                    WalkRight(player, 2, recentColor);
                    WalkUp(player, 2, recentColor);
                    WalkLeft(player, 1, recentColor);
                    WalkRight(player, 1, recentColor);
                    WalkUp(player, 1, recentColor);
                    WalkDown(player, y, recentColor);
                 } 
                 //UP
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_up" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_up"){
                   WalkLeft(player, 0, recentColor);
                    WalkRight(player, 0, recentColor);
                    WalkUp(player, y, recentColor);
                    WalkDown(player, 0, recentColor);
                 } 
                 //LEFT
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_left" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_left"){
                    WalkLeft(player, y, recentColor);
                    WalkRight(player, 0, recentColor);
                    WalkUp(player, 0, recentColor);
                    WalkDown(player, 0, recentColor);
                 }
                 //RIGHT
                 else if (player.GetComponent<SpriteRenderer>().sprite.name == "playerone_right" || player.GetComponent<SpriteRenderer>().sprite.name == "playertwo_right"){
                    WalkLeft(player, 0, recentColor);
                    WalkRight(player, y, recentColor);
                    WalkUp(player, 0, recentColor);
                    WalkDown(player, 0, recentColor);
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

       // fürn anfang: wenn die 3 touches drauf sind mach highlight
    }

    public BaseItem MakeStringIntoItem(string lastnotdestroyed){
        if(lastnotdestroyed == "Item1"){
            return Instantiate(UnitManager.Instance.Item1 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item2"){
            return Instantiate(UnitManager.Instance.Item2 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item3"){
            return Instantiate(UnitManager.Instance.Item3 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item4"){
            return Instantiate(UnitManager.Instance.Item4 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item5"){
            return Instantiate(UnitManager.Instance.Item5 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item6"){
            return Instantiate(UnitManager.Instance.Item6 ,new Vector2(0, 0), Quaternion.identity);
        } else if(lastnotdestroyed == "Item7"){
            return Instantiate(UnitManager.Instance.Item7 ,new Vector2(0, 0), Quaternion.identity);
        } else {
            return null;
        }
    }
}
