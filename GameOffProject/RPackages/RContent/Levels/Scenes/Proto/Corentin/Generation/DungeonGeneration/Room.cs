using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public int width;
    
    public int height;
    
    public int xRoom;
    
    public int yRoom;
    
    private bool updatedDoors = false;
    
    public Room(int xRoom, int yRoom)
    {
        this.xRoom = xRoom;
        this.yRoom = yRoom;
    }

    public Door leftDoor;
    
    public Door rightDoor;
    
    public Door topDoor;
    
    public Door bottomDoor;
    
    public List<Door> doors = new List<Door>();

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.LogError("No RoomController instance found.");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        
        foreach(Door door in ds)
        {
            doors.Add(door);
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = door;
                    break;
                case Door.DoorType.right:
                    rightDoor = door;
                    break;
                case Door.DoorType.top:
                    topDoor = door;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = door;
                    break;
                default:
                    break;
            }
        }
        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
        
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.right:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
            
        }
    }

    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(xRoom + 1, yRoom))
        {
            return RoomController.instance.FindRoom(xRoom + 1, yRoom);
        }
        return null;
    }
    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(xRoom - 1, yRoom))
        {
            return RoomController.instance.FindRoom(xRoom - 1, yRoom);
        }

        return null;
    }
    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(xRoom, yRoom+1))
        {
            return RoomController.instance.FindRoom(xRoom, yRoom+1);
        }

        return null;
    }
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(xRoom, yRoom-1))
        {
            return RoomController.instance.FindRoom(xRoom, yRoom-1);
        }

        return null;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    

    public Vector3 GetRoomCenter()
    {
        return new Vector3(xRoom * width, yRoom * height);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered room.");
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
    
}
