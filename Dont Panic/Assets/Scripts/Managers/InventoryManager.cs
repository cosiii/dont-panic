using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
 public bool[] isFull;
 public GameObject[] slots;

void Awake(){
        Instance = this;
    }

 public void ItemCollision(){

 }
}
