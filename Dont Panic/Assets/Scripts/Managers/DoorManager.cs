using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
  public static DoorManager Instance;
  public string lastVisitedDoor;

  public string[] keyItems1, keyItems2, keyItems3, keyItems4, keyItems5;

  void Awake(){
      Instance = this;
  }

  public void DoorCollision(){
        //Debug.Log(lastVisitedDoor);
        if (lastVisitedDoor == "door1"){
            Debug.Log("we are in front of door1");
        } else if (lastVisitedDoor == "door2"){
            Debug.Log("now door2");
        }  else if (lastVisitedDoor == "door3"){
            Debug.Log("now door3");
        } else if (lastVisitedDoor == "door4"){
            Debug.Log("now door 4");
        } else if (lastVisitedDoor == "door5"){
            Debug.Log("now door 5");
        }
      // jedem Door zwei Items zuordnen, evtl als String ausgeben
      
  }
}
