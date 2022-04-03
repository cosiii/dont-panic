using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
  public static DoorManager Instance;

  void Awake(){
      Instance = this;
  }
}
