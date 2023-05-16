using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f,1)] private float roomPercent = 0.8f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemiesPerRoom = 3;
    [SerializeField] private float enemySpawnRadius = 2f;
    [SerializeField] private GameObject exitPrefab;

    public GameObject fadeInPanel;
    public float fadeWait;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }
    private void Start()
    {
        PlayerPrefs.SetInt("ScoreValue", 0);
        StartCoroutine(FadeController());
        tilemapVisualizer.Clear();
        CorridorFirstGeneration();
    }

    public IEnumerator FadeController()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
        yield return new WaitForSeconds(fadeWait);
    }

    public void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }
    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if(floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        Vector2Int lastRoomPosition = new Vector2Int();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
            int enemiesToSpawn = UnityEngine.Random.Range(1, maxEnemiesPerRoom + 1);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * enemySpawnRadius;
                Vector3 enemyPosition = new Vector3(roomPosition.x + randomOffset.x, roomPosition.y + randomOffset.y, 0);
                Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
            }
        lastRoomPosition = roomPosition;
        }
        Vector3 exitPosition = new Vector3(lastRoomPosition.x, lastRoomPosition.y, 0);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);
        return roomPositions;
    }


    private Vector2Int GetRandomFloorTile(HashSet<Vector2Int> roomFloor)
    {
        List<Vector2Int> floorTiles = new List<Vector2Int>(roomFloor);
        return floorTiles[UnityEngine.Random.Range(0, floorTiles.Count)];
    }
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGeneration.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);

        }
    }
}
