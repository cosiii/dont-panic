using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile floorTile, itemTile;
    [SerializeField] private Transform _cam;
    [SerializeField] private int x1, y1;

    private Dictionary<Vector2, Tile> tiles;
    

void Awake(){
    Instance = this;
}


public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                /* spawn items
                if (x == x1 && y == y1 || x ==2 && y == 1 || x ==6 && y == 1){
    	        var spawnedTile = Instantiate(itemTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y); 
                tiles[new Vector2(x,y)] = spawnedTile;
                } else { // spawn floor */
                var spawnedTile = Instantiate(floorTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y); 
                tiles[new Vector2(x,y)] = spawnedTile;
               // }
                
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnPlayers);
    }
public Tile GetPlayerSpawnTile(){
    // left side of map and is walkable
    return tiles.Where(t => t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=> Random.value).First().Value;
    }

    public Tile GetPatrolSpawnTile(){
    // left side of map and is walkable
    return tiles.Where(t => t.Key.x > _width/2 && t.Value.Walkable).OrderBy(t=> Random.value).First().Value;
    }

    public Tile GetItemSpawnTile(){
    // left side of map and is walkable
    return tiles.Where(t => t.Key.x < _width && t.Value.Walkable).OrderBy(t=> Random.value).First().Value;
    }
    
public Tile GetTileAtPosition(Vector2 pos){
    if(tiles.TryGetValue(pos, out var tile)){
        return tile;
    }
    return null;
}
 
}
