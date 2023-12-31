// Created by Kabourlix Cendrée on 20/11/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public class GridControler : MonoBehaviour
    {
        public Room room;

        [Serializable]
        public struct Grid
        {
            public int columns, rows;
            public float verticalOffset, horizontalOffset;
        }

        public Grid grid;
        public GameObject gridTile;
        public List<Vector2> availablePoints = new();

        private void Awake()
        {
            room = GetComponentInParent<Room>();
            grid.columns = room.width - 2;
            grid.rows = room.height - 2;
        }

        private void Start()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            Vector3 temp = room.transform.localPosition;
            grid.verticalOffset += temp.y;
            grid.horizontalOffset += temp.x;

            for (int y = 0; y < grid.rows; y++)
            {
                for (int x = 0; x < grid.columns; x++)
                {
                    GameObject go = Instantiate(gridTile, transform);
                    go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset),
                        y - (grid.rows - grid.verticalOffset));
                    go.name = "GridTile " + x + " " + y;
                    availablePoints.Add(go.transform.position);
                    go.SetActive(false);
                }
            }

            GetComponentInParent<ObjectRoomSpawner>().InitializeObjectSpawning();
        }
    }
}