using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
 public static InventoryManager Instance;
 public bool[] isFull;
 public GameObject[] slots;

 public GameObject inventoryPoint;

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){
Debug.Log("item picked up by " + MenuManager.Instance.selectedPlayerObject.GetComponentInChildren<Text>().text);
// Debug.Log("" + Tile.Instance.OccupiedUnit.gameObject);
// SetUnit(UnitManager.Instance.SelectedPlayer);
for (int i = 0; i < slots.Length; i++)
{
    if(isFull[i] == false){ // item can be added to inventory
        // parented to slots[i]
        
        Instantiate(inventoryPoint, slots[i].transform, false);
        isFull[i] = true;
        break;
    }
}
 }

}
