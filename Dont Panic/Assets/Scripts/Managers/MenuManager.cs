using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public static MenuManager Instance;
   [SerializeField] private GameObject selectedPlayerObject, tileObject, tileUnitObject;

void Awake(){
    Instance = this;
}

public void ShowTileInfo(Tile tile){
    if(tile == null){
        tileObject.SetActive(false);
        tileUnitObject.SetActive(false);
        return;
    }

    tileObject.GetComponentInChildren<Text>().text = tile.TileName;
    tileObject.SetActive(true);
    // if tile has an pccupied unit
    if(tile.OccupiedUnit){
        tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
        tileUnitObject.SetActive(true);
    }
}
public void ShowSelectedPlayer(BasePlayer player){
    if(player == null){
        selectedPlayerObject.SetActive(false);
        return;
    }
    
selectedPlayerObject.GetComponentInChildren<Text>().text = player.UnitName;
selectedPlayerObject.SetActive(true);
}
}
