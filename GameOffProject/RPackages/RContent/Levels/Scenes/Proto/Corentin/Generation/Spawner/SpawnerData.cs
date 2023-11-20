
using UnityEngine;



namespace Rezoskour.Content

{
    [CreateAssetMenu(fileName = "SpawerData.asset", menuName = "Spawer/Spawer", order = 1)]
    public class SpawnerData : ScriptableObject
    {
        public GameObject itemToSpawn;
        public int minSpawn;
        public int maxSpawn;
    }
}
