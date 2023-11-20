using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
    private void SpawnRooms(List<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        
        foreach (var roomPosition in rooms)
        {
          RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomPosition.x, roomPosition.y);
        }
    }
}
