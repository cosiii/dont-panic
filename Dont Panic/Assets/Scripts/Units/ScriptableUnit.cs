using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// what we need in the Resources Folder in Unity
[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
   public Faction Faction;
   public BaseUnit UnitPrefab;
}

public enum Faction {
    Player = 0,
    Patrol = 1,
    Item = 2
}