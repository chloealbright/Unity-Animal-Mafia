using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] public int _width, _height;

    [SerializeField] private Tile _floorTile, _wallTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    void Awake() {
        Instance = this;
    }

    // public void GenerateGrid()
    // {
    //     // var tileSize = 0.16f;
    //     _tiles = new Dictionary<Vector2, Tile>();
    //     for (int x = 0; x < _width; x++) {
    //         for (int y = 0; y < _height; y++) {
    //             var randomTile = Random.Range(0,6) == 3? _wallTile : _floorTile;
    //             // var xPos = (x * tileSize) - (tileSize / 2f);
    //             // var yPos = (y * tileSize) - (tileSize / 2f);
    //             var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
    //             spawnedTile.name = $"Tile {x} {y}";
 
    //             spawnedTile.Init(x, y);
 
 
    //             _tiles[new Vector2(x, y)] = spawnedTile;
    //         }
    //     }

    //     _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

    //     GameManager.Instance.ChangeState(GameState.SpawnCharacters);
    // }
    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                var randomTile = Random.Range(0, 6) == 3 ? _wallTile : _floorTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

              
                spawnedTile.Init(x,y);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        GameManager.Instance.ChangeState(GameState.SpawnCharacters);
    }

    public Tile GetCharacterSpawnTile() {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}

