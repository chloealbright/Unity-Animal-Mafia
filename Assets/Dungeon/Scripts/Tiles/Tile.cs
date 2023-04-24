using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;


    public virtual void Init(int x, int y)
    {
      
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        // MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        // MenuManager.Instance.ShowTileInfo(null);
    }

   void OnMouseDown() {
    if(GameManager.Instance.GameState != GameState.CharactersTurn) return;
    if (OccupiedUnit != null) {
        if(OccupiedUnit.Type == Type.Character) UnitManager.Instance.SetSelectedCharacter((BaseCharacter)OccupiedUnit);
        else {
            if (UnitManager.Instance.SelectedCharacter != null) {
                var enemy = (BaseEnemy) OccupiedUnit;
                Destroy(enemy.gameObject);
                UnitManager.Instance.SetSelectedCharacter(null);
            }
        }
    }
    else {
        if (UnitManager.Instance.SelectedCharacter != null) {
            SetUnit(UnitManager.Instance.SelectedCharacter);
            UnitManager.Instance.SetSelectedCharacter(null);
        }
    }

    // Move the player unit to the clicked tile
    var targetTile = this;
    UnitManager.Instance.MoveUnit(targetTile);
    // MenuManager.Instance.UpdateMoveableTiles();
}

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
    public bool IsReachable()
    {
        if (OccupiedUnit != null || !Walkable) return false;
        var playerUnit = UnitManager.Instance.SelectedCharacter;
        var playerPos = playerUnit.OccupiedTile.transform.position;
        var tilePos = transform.position;
        return Mathf.Abs(tilePos.x - playerPos.x) + Mathf.Abs(tilePos.y - playerPos.y) == 1;
    }
}