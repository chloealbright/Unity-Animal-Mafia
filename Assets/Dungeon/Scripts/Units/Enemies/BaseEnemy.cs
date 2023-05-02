using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BaseEnemy : BaseUnit
{
    private Tile previousTile1;
    private Tile previousTile2;
    // Start is called before the first frame update
    void Start()
    {

    }
public void MoveRandomly() {
    var currentTile = OccupiedTile;
    var neighboringTiles = currentTile.GetNeighboringTiles(OccupiedTile);
    Debug.Log(neighboringTiles);

    if (neighboringTiles.Count() > 0) {
        var randomIndex = Random.Range(0, neighboringTiles.Count());
        var randomTile = neighboringTiles.ElementAt(randomIndex);
        currentTile.OccupiedUnit = null;
        randomTile.SetUnit(this);
    }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
