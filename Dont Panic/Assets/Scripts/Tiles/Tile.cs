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
        MenuManager.Instance.ShowTileInfo(this);
    } 

    void OnMouseExit(){ //works only on clicks now
        MenuManager.Instance.ShowTileInfo(null);
    }

    void OnMouseDown(){

        // PLAYER ONE
        // only care about it when its player ones turn
        //if(GameManager.Instance.GameState != GameState.Player1Turn) return;

        // when its occupied by a player or anything else
        if( OccupiedUnit != null ){ //when tile is occupied
            if(OccupiedUnit.Faction == Faction.Player){
                if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 1" ){
                    UnitManager.Instance.SetSelectedPlayer((Player1)OccupiedUnit);
                    Player1.Instance.highlight.SetActive(true);
                    ShowWalkableTiles(Player1.Instance);
                } else if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" ){
                    UnitManager.Instance.SetSelectedPlayer((Player2)OccupiedUnit);
                    Player2.Instance.highlight.SetActive(true);
                }


            } 
            else { 
                if(UnitManager.Instance.SelectedPlayer != null){ // if we have a selected player AND we click on another occupied unit
                    
                        // COLLISION ITEM
                        if(OccupiedUnit.Faction == Faction.Item){ // just destroy it when the inventory of the player isnt completely full
                            if(InventoryManager.Instance.inventoryIsFullPlayerOne == false && GameManager.Instance.GameState == GameState.Player1Turn ||
                            InventoryManager.Instance.inventoryIsFullPlayerTwo == false && GameManager.Instance.GameState == GameState.Player2Turn ){
                               DestroyUnit();
                            }
                        InventoryManager.Instance.ItemCollision();
                        }
                        
                        
                        // COLLISION DOOR
                        if(OccupiedUnit.Faction == Faction.Door){
                            //Debug.Log("just one?");
                            DoorManager.Instance.lastVisitedDoor = OccupiedUnit.UnitName;
                            LastDoor = OccupiedUnit.UnitName;
                            DoorManager.Instance.DoorCollision();
                            playerOnDoor = true;
                        }
                    
                    

                    //deselect selected Unit
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                    UnitManager.Instance.SetSelectedPlayer(null);

                    // dehighlight all
                    Player1.Instance.highlight.SetActive(false);
                    Player2.Instance.highlight.SetActive(false);

                    ChangePlayerTurn();
                }

            }

    	    
        }
        else {
            // already got a selected Unit
            if(UnitManager.Instance.SelectedPlayer != null){

                
                    //deselect selected Unit
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                     UnitManager.Instance.SetSelectedPlayer(null);

                    // dehighlight all
                    Player1.Instance.highlight.SetActive(false);
                    Player2.Instance.highlight.SetActive(false);

                ChangePlayerTurn();
                
                if (playerOnDoor ==true){
                    Debug.Log("player on door the second time");
                    DoorManager.Instance.lastVisitedDoor = LastDoor;
                            DoorManager.Instance.DoorCollision();
                }
                

            }
        }


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

    public void GetDoorName(){
    }

    public void ChangePlayerTurn(){
    if(GameManager.Instance.GameState == GameState.Player1Turn){
        UpdatePosition(Player1.Instance);
        HideWalkableTiles();
        GameManager.Instance.ChangeState(GameState.Player2Turn);
        } else if(GameManager.Instance.GameState == GameState.Player2Turn){
        UpdatePosition(Player2.Instance);
        GameManager.Instance.ChangeState(GameState.Player1Turn);
        }
    }

    public void UpdatePosition(BasePlayer player){
    char cx = player.OccupiedTile.name[5];
    char cy = player.OccupiedTile.name[7];
    player.posx = cx -48;
    player.posy = cy -48;
    Debug.Log("player stands on Tile " + player.posx + " " + player.posy);
    }

    public void ShowWalkableTiles(BasePlayer player){
    // die tiles nur walkable die in der nähe sind
    
    
    if(Player1.Instance.OccupiedTile.name == "Tile 5 5"){
                }


    for (int x = 0; x < player.walkingDistance; x++)
        {
            for (int y = 0; y < player.walkingDistance; y++)
            {
                //hier noch prüfen dass es im dictionary is 
                GridManager.Instance.tiles[new Vector2(Player1.Instance.posx - x, Player1.Instance.posy - y )].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(Player1.Instance.posx + x, Player1.Instance.posy - y )].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(Player1.Instance.posx - x, Player1.Instance.posy + y )].highlight.SetActive(true);
                GridManager.Instance.tiles[new Vector2(Player1.Instance.posx + x, Player1.Instance.posy + y )].highlight.SetActive(true);
                
            }
        }
    }

    public void HideWalkableTiles(){

        for (int x = 0; x < GridManager.Instance._width; x++)
        {
            for (int y = 0; y < GridManager.Instance._height; y++)
            {
                GridManager.Instance.tiles[new Vector2(x, y )].highlight.SetActive(false);
                
            }
        }

    }
}
