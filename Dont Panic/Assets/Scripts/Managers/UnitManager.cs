using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> units;
    void Awake(){
        Instance = this;
        // goes through Units Folder and look thorugh all the subfolders fpr any type of scriptable unit
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnPlayers(){
        var playerCount =2;
        for (int i = 0; i < playerCount; i++)
        {
            var randomPrefab = GetRandomUnit<BasePlayer>(Faction.Player);
            var spawnedHero = Instantiate(randomPrefab);
        }
    }


    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        // going thorugh our list, wanting all the units of the faction we*re telling it, randomly shuffling them around (why?), take the first and just get the Prefab
        return (T)units.Where(u => u.Faction == faction).OrderBy( o => Random.value).First().UnitPrefab;
    }
}
