using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left,
        right,
        top,
        bottom
    }
    public DoorType doorType;
    
    public GameObject doorCollider;
    
    private float widthOffset = 3f;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y - widthOffset,1);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector3(transform.position.x, transform.position.y + widthOffset,1);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector3(transform.position.x + widthOffset, transform.position.y,1 );
                    break;
                case DoorType.left:
                    player.transform.position = new Vector3(transform.position.x - widthOffset, transform.position.y,1);
                    break;
                
            }
        }
        
    }
}
