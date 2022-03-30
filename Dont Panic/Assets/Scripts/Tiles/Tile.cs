using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;  // affectively private, but derived tiles can access it
    [SerializeField]private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => isWalkable && OccupiedUnit == null; // checks if tile is walkable and not occupied

    public virtual void Init(int x, int y)  // will run on every tile, but each individula tile has the option to overwrite it
    {
      
    }


    public void OnMouseEnter(){  //works only on clicks now
        MenuManager.Instance.ShowTileInfo(this);
        Debug.Log("on mouse enter");
    } 

    void OnMouseExit(){ //works only on clicks now
        MenuManager.Instance.ShowTileInfo(null);
        Debug.Log("on mouse exit");
    }

    void OnMouseDown(){
        Debug.Log("on mouse down");

        // PLAYER ONE
        // only care about it when its player ones turn
        //if(GameManager.Instance.GameState != GameState.Player1Turn) return;

        // when its occupied by a player or anything else
        if( OccupiedUnit != null ){ //when tile is occupied
            if(OccupiedUnit.Faction == Faction.Player){
                if(GameManager.Instance.GameState == GameState.Player1Turn && OccupiedUnit.UnitName == "player 1" ){
                    UnitManager.Instance.SetSelectedPlayer((Player1)OccupiedUnit);
                } else if(GameManager.Instance.GameState == GameState.Player2Turn && OccupiedUnit.UnitName == "player 2" ){
                    UnitManager.Instance.SetSelectedPlayer((Player2)OccupiedUnit);
                }
                
            } 
            else { 
                if(UnitManager.Instance.SelectedPlayer != null){ // if we have a selected player AND we click on another occupied unit
                    Destroy(OccupiedUnit.gameObject);
                    //deselect selected Unit
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                     UnitManager.Instance.SetSelectedPlayer(null);
                     ChangePlayerTurn();
                }

            }
        }
        else {
            // already got a selected Unit
            if(UnitManager.Instance.SelectedPlayer != null){
               
                    SetUnit(UnitManager.Instance.SelectedPlayer);
                     UnitManager.Instance.SetSelectedPlayer(null);

                ChangePlayerTurn();
                

            }
        }


    } 

    public void SetUnit(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position =transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void ChangePlayerTurn(){
if(GameManager.Instance.GameState == GameState.Player1Turn){
                     GameManager.Instance.ChangeState(GameState.Player2Turn);
                } else if(GameManager.Instance.GameState == GameState.Player2Turn){
                     GameManager.Instance.ChangeState(GameState.Player1Turn);
                }
    }
}
