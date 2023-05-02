using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    public int GridX { get; private set; }
    public int GridY { get; private set; }
    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    void Start() {
    }
    public virtual void Init(int x, int y)
    {
        GridX = x;
        GridY = y;
    }
public List<Tile> GetNeighboringTiles(Tile currentTile)
{
    List<Tile> neighbors = new List<Tile>();
    // Check top neighbor
    // Check top neighbor
var topNeighbor = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentTile.GridX, (int)currentTile.GridY + 1));
if (topNeighbor != null && topNeighbor.Walkable)
{
    neighbors.Add(topNeighbor);
}

// Check bottom neighbor
var bottomNeighbor = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentTile.GridX, (int)currentTile.GridY - 1));
if (bottomNeighbor != null && bottomNeighbor.Walkable)
{
    neighbors.Add(bottomNeighbor);
}

// Check left neighbor
var leftNeighbor = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentTile.GridX - 1, (int)currentTile.GridY));
if (leftNeighbor != null && leftNeighbor.Walkable)
{
    neighbors.Add(leftNeighbor);
}

// Check right neighbor
var rightNeighbor = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentTile.GridX + 1, (int)currentTile.GridY));
if (rightNeighbor != null && rightNeighbor.Walkable)
{
    neighbors.Add(rightNeighbor);
}


    return neighbors;
}


    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

void OnMouseDown() {
    if(GameManager.Instance.GameState != GameState.HeroesTurn) return;
    if (OccupiedUnit != null) {
        if(OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
        else {
            if (UnitManager.Instance.SelectedHero != null) {
                var enemy = (BaseEnemy) OccupiedUnit;
                Destroy(enemy.gameObject);
                GameManager.Instance.ChangeState(GameState.EnemiesTurn);
            }
        }
    }
    else {
        if (UnitManager.Instance.SelectedHero != null) {
            // Check if the clicked tile is adjacent to the selected hero's tile
            var selectedHeroTile = UnitManager.Instance.SelectedHero.OccupiedTile;
            var clickedTile = this;
            if (selectedHeroTile != clickedTile && GetDistance(selectedHeroTile, clickedTile) < 2 && _isWalkable) {
                SetUnit(UnitManager.Instance.SelectedHero);
                GameManager.Instance.ChangeState(GameState.EnemiesTurn);
            }
        }
    }
}

// Helper method to calculate the distance between two tiles
private int GetDistance(Tile tile1, Tile tile2) {
    return (int)Mathf.Sqrt(Mathf.Pow(tile1.transform.position.x - tile2.transform.position.x, 2) + Mathf.Pow(tile1.transform.position.y - tile2.transform.position.y, 2));
}



    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}