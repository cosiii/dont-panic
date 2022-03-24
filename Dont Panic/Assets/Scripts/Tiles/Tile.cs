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
        // only care about it when its players turn
        if(GameManager.Instance.GameState != GameState.PlayersTurn) return;

        // when its occupied
        if( OccupiedUnit != null){
            if(OccupiedUnit.Faction == Faction.Player){
                UnitManager.Instance.SetSelectedPlayer((BasePlayer)OccupiedUnit);
            } 
            else {
                if(UnitManager.Instance.SelectedPlayer != null){
                    var patrol = (BasePatrol) OccupiedUnit;
                    Destroy(patrol.gameObject);
                    //deselect selected Unit
                    UnitManager.Instance.SetSelectedPlayer(null);
                }
            }
        }
        else {
            // already got a selected Unit
            if(UnitManager.Instance.SelectedPlayer != null){
                SetUnit(UnitManager.Instance.SelectedPlayer);
                UnitManager.Instance.SetSelectedPlayer(null);

            }
        }

    } 

    public void SetUnit(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position =transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
