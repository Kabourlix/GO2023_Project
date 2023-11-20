
using System;
using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rezoskour.Content
{
    public class ObjectRoomSpawner : MonoBehaviour
    {
        [Serializable]
        public struct RandomSpawner
        {
            public string name;
            public SpawnerData spawnerData;
        }

        public GridControler grid;
        public RandomSpawner[] spawnerData;

        private void Start()
        {
            //grid = GetComponentInChildren<GridControler>();
        }
        void SpawnObjects(RandomSpawner data)
        {
            int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn +1);
            
            for (int i = 0; i < randomIteration; i++)
            {
                int randomIndex = Random.Range(0, grid.availablePoints.Count - 1);
                Vector2 randomPosition = grid.availablePoints[randomIndex];
                grid.availablePoints.RemoveAt(randomIndex);
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, transform);
                go.transform.position = randomPosition;
                go.name = data.name + " " + i;
            }
        }
        public void InitializeObjectSpawning()
        {
            foreach (RandomSpawner data in spawnerData)
            {
                SpawnObjects(data);
            }
        }
        
    }
}