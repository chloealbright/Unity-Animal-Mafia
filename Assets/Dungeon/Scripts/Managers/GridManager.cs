using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _floorTile, _wallTile, _exitTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;
    public int Height { get { return _height; } }
    public int Width { get { return _width; } }

    void Awake() {
        Instance = this;
    }

    public void GenerateGrid()
    {
    _tiles = new Dictionary<Vector2, Tile>();
    bool exitTileGenerated = false;
    for (int x = 0; x < _width; x++)
    {
        for (int y = 0; y < _height; y++)
        {
            Tile spawnedTile = _floorTile;

            if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
            {
                spawnedTile = _wallTile;
            }
            else
            {
                if (Random.Range(0, 6) == 3)
                {
                    spawnedTile = _wallTile;
                }
                else if (!exitTileGenerated && Random.Range(0, 100) == 0)
                {
                    spawnedTile = _exitTile;
                    exitTileGenerated = true;
                }
            }

            var tilePosition = new Vector3(x, y, 0f);
            var tileRotation = Quaternion.identity;
            var tileObject = Instantiate(spawnedTile, tilePosition, tileRotation);
            tileObject.name = $"Tile {x} {y}";
            tileObject.Init(x, y);
            _tiles[new Vector2(x, y)] = tileObject;
        }
    }
    _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }
    
    public Tile GetHeroSpawnTile() {
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