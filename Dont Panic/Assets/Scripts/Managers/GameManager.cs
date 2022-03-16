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
                break;
            case GameState.SpawnPlayers:
            UnitManager.Instance.SpawnPlayers();
                break;
            case GameState.SpawnPatrol:
            UnitManager.Instance.SpawnPatrols();
                break;
            case GameState.PlayersTurn:
                break;
            case GameState.PatrolTurn:
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
    PlayersTurn = 3,
    PatrolTurn = 4
} 
 