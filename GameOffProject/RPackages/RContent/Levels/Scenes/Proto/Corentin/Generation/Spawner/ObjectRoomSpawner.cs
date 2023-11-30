// Created by Kabourlix Cendrée on 20/11/2023

using System;
using PlasticGui.Configuration.CloudEdition.Welcome;
using SDKabu.KCore;
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
            public EnemyData enemyData;
        }

        private IEnemyManager enemyManager;
        public GridControler grid;
        public RandomSpawner[] spawnerData;

        private void Start()
        {
            enemyManager = KServiceInjection.Get<IEnemyManager>();
            //grid = GetComponentInChildren<GridControler>();
        }

        private void SpawnObjects(RandomSpawner data)
        {
            int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

            for (int i = 0; i < randomIteration; i++)
            {
                int randomIndex = Random.Range(0, grid.availablePoints.Count - 1);
                Vector2 randomPosition = grid.availablePoints[randomIndex];
                grid.availablePoints.RemoveAt(randomIndex);

                Enemy enemy = enemyManager.SpawnEnemy(data.enemyData, randomPosition, Quaternion.identity);
                enemy.SetActive(false);
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