using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer _renderer;  // affectively private, but derived tiles can access it
    [SerializeField]private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => isWalkable && OccupiedUnit == null; // checks if tile is walkable and not occupied

    public virtual void Init(int x, int y)  // will run on every tile, but each individula tile has the option to overwrite it
    {
      
    }

    public void SetUnit(BaseUnit unit){
        if(unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position =transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
