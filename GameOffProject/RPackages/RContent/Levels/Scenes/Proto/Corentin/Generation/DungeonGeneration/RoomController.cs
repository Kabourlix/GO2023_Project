using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public String name;
    public int xRi;
    public int yRi;
}


public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    private String currentWorldName = "Basement";

    private RoomInfo currentLoadRoomData;
    
    Room currentRoom;
    
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    
    public List<Room> loadedRooms = new List<Room>();
    
    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;
    
    bool updatedRoom = false;
    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        UpdateRoomQueue();
    }
    
    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }else if(spawnedBossRoom && !updatedRoom)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRoom = true;
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.xRoom, bossRoom.yRoom);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.xRoom == tempRoom.xRoom && r.yRoom == tempRoom.yRoom);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.xRoom, tempRoom.yRoom);
        }
    }
    public void LoadRoom(String name,int x, int y)
    {
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.xRi = x;
        newRoomData.yRi = y;
        loadRoomQueue.Enqueue(newRoomData);
    }
    
    IEnumerator LoadRoomRoutine(RoomInfo roomInfo)
    {
        String roomName = currentWorldName + roomInfo.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadRoomData.xRi, currentLoadRoomData.yRi))
        {
            room.transform.position = new Vector3(currentLoadRoomData.xRi * room.width, currentLoadRoomData.yRi * room.height);
            room.xRoom = currentLoadRoomData.xRi;
            room.yRoom = currentLoadRoomData.yRi;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.xRoom + ", " + room.yRoom;
            room.transform.parent = transform;
            isLoadingRoom = false;
            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }
            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
        
    }
    
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Exists(item => item.xRoom == x && item.yRoom == y);
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.xRoom == x && item.xRoom == y);
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }

    
}
