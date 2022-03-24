using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;

    public BasePlayer SelectedPlayer;
    public BasePlayer Player1;
    public BasePlayer Player2;
    

    void Awake(){
        Instance = this;
        // goes through Units Folder and look thorugh all the subfolders fpr any type of scriptable unit
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnPlayers(){
        var playerCount =2;
        
        for (int i = 0; i < playerCount; i++)
        {
            // Player 1 & 2
            var player = Player1;
            if(i == 0){
                  player = Player1;
            } else if(i == 1){
                player = Player2;
            }
            // spawning player
            var spawnedPlayer = Instantiate(player);
            // get the tile of the player from GridManager
            var randomSpawnTile = GridManager.Instance.GetPlayerSpawnTile();
            randomSpawnTile.SetUnit(spawnedPlayer);

        }

        GameManager.Instance.ChangeState(GameState.SpawnPatrol);
    }

    public void SpawnPatrols(){
        var patrolCount =1;
        for (int i = 0; i < patrolCount; i++)
        {
            var randomPrefab = GetRandomUnit<BasePatrol>(Faction.Patrol);
            var spawnedPatrol = Instantiate(randomPrefab);
            // get the tile of the player from GridManager
            var randomSpawnTile = GridManager.Instance.GetPatrolSpawnTile();

            randomSpawnTile.SetUnit(spawnedPatrol);

        }

        GameManager.Instance.ChangeState(GameState.Player1Turn);
    }


    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        // going thorugh our list, wanting all the units of the faction we*re telling it, randomly shuffling them around (why?), take the first and just get the Prefab
        return (T)units.Where(u => u.Faction == faction).OrderBy( o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedPlayer(BasePlayer player){
        SelectedPlayer = player;
        MenuManager.Instance.ShowSelectedPlayer(player);
    }
}
