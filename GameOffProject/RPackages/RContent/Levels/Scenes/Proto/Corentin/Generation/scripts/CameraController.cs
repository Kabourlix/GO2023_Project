using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    public Room currentRoom;
    
    public float moveSpeedWhenRoomChange;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if(currentRoom == null)
        {
            return;
        }
        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
    }
    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }
        Vector3 targetPos = currentRoom.GetRoomCenter();
        targetPos.z = transform.position.z;
        return targetPos;
    }
    
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
