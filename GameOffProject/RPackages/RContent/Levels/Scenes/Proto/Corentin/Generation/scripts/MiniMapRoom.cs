using System;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapRoom : MonoBehaviour
{
    public Door leftDoor;
    
    public Door rightDoor;
    
    public Door topDoor;
    
    public Door bottomDoor;
    
    public List<Door> doors = new List<Door>();

    public int xPos;    
    public int yPos;
    
    public bool isVisited = false;

    private void Start()
    {
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
    }
    public void HideRoom()
    {
        if (!isVisited)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (!GetLeft())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.right:
                    if (!GetRight())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                    if (!GetTop())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (!GetBottom())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
            
        }
    }

    public bool GetRight()
    {
        int tempx = xPos + 1;
        if(RoomController.instance.DoesRoomExist(tempx, yPos))
        {
            Debug.Log("Room exists to the right."+ xPos + " " + yPos);
            return true;
        }
        return false;
    }
    public bool GetLeft()
    {
        int tempx = xPos - 1;
        if(RoomController.instance.DoesRoomExist(tempx, yPos))
        {
            Debug.Log("Room exists to the left."+ xPos + " " + yPos);
            return true;
        }
        return false;
    }
    public bool GetTop()
    {
        if(RoomController.instance.DoesRoomExist(xPos, yPos+1))
        {
            return true;
        }
        return false;
    }
    public bool GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(xPos, yPos-1))
        {
            return true;
        }
        return false;
    }
    
}
