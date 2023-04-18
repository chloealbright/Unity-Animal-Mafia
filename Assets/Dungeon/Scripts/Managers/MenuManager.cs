using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedCharacterObject,_tileObject,_tileUnitObject;

    void Awake() {
        Instance = this;
    }

    public void ShowTileInfo(Tile tile) {
        if (tile == null)
        {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
        }
    }

    public void ShowSelectedCharacter(BaseCharacter character) {
        if (character == null) {
            _selectedCharacterObject.SetActive(false);
            return;
        }

        _selectedCharacterObject.GetComponentInChildren<Text>().text = character.UnitName;
        _selectedCharacterObject.SetActive(true);
    }
}
