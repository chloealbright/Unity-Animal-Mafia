using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public static class ProceduralGeneration
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var prevousPosition = startPosition;
        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = prevousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            prevousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for (int i =0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,0), 
        new Vector2Int(0,-1), 
        new Vector2Int(-1,0) 
    };
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), 
        new Vector2Int(1,-1), 
        new Vector2Int(-1,-1), 
        new Vector2Int(-1,1) 
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),  
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),  
        new Vector2Int(-1,0),
        new Vector2Int(-1,1) 
        
    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}