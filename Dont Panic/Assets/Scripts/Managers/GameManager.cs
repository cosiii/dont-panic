using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    void Awake(){
        Instance = this;
    }

    void Start(){
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState){
        GameState = newState;
        switch (newState){
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                AudioManager.Instance.Play("scary");
                break;
            case GameState.SpawnPlayers:
            UnitManager.Instance.SpawnDoors();
            UnitManager.Instance.SpawnPlayers();
            UnitManager.Instance.SpawnItems();
                break;
            case GameState.SpawnPatrol:
            UnitManager.Instance.SpawnPatrols();
                break;
            case GameState.Player1Turn:
                 MenuManager.Instance.ShowPlayersTurn();
                break;
            case GameState.Player2Turn:
                 MenuManager.Instance.ShowPlayersTurn();
                break;
            case GameState.GameWon:
                MenuManager.Instance.ShowExitText();
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
             
        
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnPlayers = 1,
    SpawnPatrol = 2,
    Player1Turn = 3,
    Player2Turn = 4,
    GameWon = 5
} 
 