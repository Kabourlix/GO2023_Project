using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Random = UnityEngine.Random;

public class RoomInfo
{
    public String name;
    public int xRi;
    public int yRi;
}


public class RoomController : MonoBehaviour
{
    public Camera miniMapCamera;
    public static RoomController instance;
    public GameObject roomMiniMap;

    private String currentWorldName = "Basement";

    private RoomInfo currentLoadRoomData;
    
    Room currentRoom;
    
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    
    public List<Room> loadedRooms = new List<Room>();
    public List<MiniMapRoom> loadedRoomsMM = new List<MiniMapRoom>();
    
    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;
    
    bool updatedRoom = false;
    
    bool bossIsSpawned = false;
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
            }else if(spawnedBossRoom && !updatedRoom && bossIsSpawned)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }

                foreach (MiniMapRoom roomMM in loadedRoomsMM)
                {
                    roomMM.RemoveUnconnectedDoors();
                    roomMM.HideRoom();
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
        yield return new WaitForSeconds(1f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.xRoom, bossRoom.yRoom);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.xRoom == tempRoom.xRoom && r.yRoom == tempRoom.yRoom);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.xRoom, tempRoom.yRoom);
            bossIsSpawned = true;
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
            var roomGO = Instantiate(roomMiniMap, new Vector3(currentLoadRoomData.xRi * 3.5f, currentLoadRoomData.yRi * 2.3f), Quaternion.identity);
            roomGO.transform.parent = transform;
            MiniMapRoom miniMapRoom = roomGO.GetComponent<MiniMapRoom>();
            miniMapRoom.xPos = currentLoadRoomData.xRi;
            miniMapRoom.yPos = currentLoadRoomData.yRi;
            if(currentLoadRoomData.name == "Start")
            {
                miniMapRoom.isVisited = true;
            }
            else
            {
                miniMapRoom.isVisited = false;
            }
            roomGO.name = "Minimap" + "-" + currentLoadRoomData.name + " " + miniMapRoom.xPos + ", " + miniMapRoom.yPos;
            loadedRoomsMM.Add(miniMapRoom);
            room.transform.position = new Vector3((currentLoadRoomData.xRi * room.width),
                (currentLoadRoomData.yRi * room.height));
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
    
    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Empty",
            "Basic1"
        };
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
        UpdateMiniMap();
        UpdateRooms();
    }

    private void UpdateMiniMap()
    {
        miniMapCamera.transform.position = new Vector3(currentRoom.xRoom * 3.5f, currentRoom.yRoom * 2.3f, -10);
        foreach (MiniMapRoom room in loadedRoomsMM)
        {
            if (currentRoom != null)
            {
                if (room.xPos == currentRoom.xRoom && room.yPos == currentRoom.yRoom)
                {
                    room.isVisited = true;
                }
            }
            room.HideRoom();
        }
        
    }

    private void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currentRoom != null)
            {
                //Get all enemies in the room
                //foreach enemy enemy.notInRoom = true
            }
            else
            {
                //Get all enemies in the room
                //foreach enemy enemy.notInRoom = false
            }
        }
    }
}
