using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseCharacter SelectedCharacter;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }
     public void SpawnCharacters() {
        var characterCount = 1;

        for (int i = 0; i < characterCount; i++) {
            var randomPrefab = GetRandomUnit<BaseCharacter>(Type.Character);
            var spawnedCharacter = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetCharacterSpawnTile();

            randomSpawnTile.SetUnit(spawnedCharacter);
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Type.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.CharactersTurn);
    }
    public void MoveUnit(Tile targetTile)
{
    var playerUnit = SelectedCharacter;
    if (playerUnit == null) return;
    var currentTile = playerUnit.OccupiedTile;
    if (targetTile.IsReachable())
    {
        currentTile.OccupiedUnit = null;
        targetTile.SetUnit(playerUnit);
    }
}
    private T GetRandomUnit<T>(Type type) where T : BaseUnit {
        return (T)_units.Where(u => u.Type == type).OrderBy(o => Random.value).First().UnitPrefab;
    }
    public void SetSelectedCharacter(BaseCharacter character) {
        SelectedCharacter = character;
        // MenuManager.Instance.ShowSelectedCharacter(character);
    }
}