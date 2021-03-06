using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] public int _width, _height;
    [SerializeField] public Tile floorTile;
    [SerializeField] private Transform _cam;

    public Dictionary<Vector2, Tile> tiles;
     [SerializeField] public Color colorPlayer1, colorPlayer2;

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
                var spawnedTile = Instantiate(floorTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y); 
                tiles[new Vector2(x,y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnPlayers);
    }
   

    public Tile GetSpawnTile(float x, float y){
    // left side of map and is walkable
    return tiles.Where(t => t.Key.x == x && t.Key.y == y).OrderBy(t=> Random.value).First().Value;
    }

    public Tile GetPatrolSpawnTile(){
    // right side of map and is walkable
    return tiles.Where(t => t.Key.x > _width/2 && t.Value.Walkable).OrderBy(t=> Random.value).First().Value;
    }
    public Tile GetItemSpawnTile(){
    //map and is walkable
    return tiles.Where(t => t.Key.x < _width && t.Key.y < _height && t.Value.Walkable).OrderBy(t=> Random.value).First().Value;
    }

     public Tile GetDoorSpawnTile(int x, int y){
    //map and is walkable
    return tiles.Where(t => t.Key.x == x && t.Key.y == y ).OrderBy(t=> Random.value).First().Value;
    }

public Tile GetTileAtPosition(Vector2 pos){
    if(tiles.TryGetValue(pos, out var tile)){
        return tile;
    }
    return null;
}
 
}
