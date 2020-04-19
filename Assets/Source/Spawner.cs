using UnityEngine;

namespace Assets.Source
{
    public enum SpawnerType
    {
        Enemy,
        Debuff,
    }

    /// <summary>
    /// Spawner object that can instantiate enemies
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public GameObject SpawnEffect;
        public SpawnerType SpawnerType = SpawnerType.Enemy;

        /// <summary>
        /// Spawns an enemy
        /// </summary>
        /// <param name="prefab"></param>
        public void Spawn(GameObject prefab)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(Instantiate(SpawnEffect, transform.position, Quaternion.identity), 3F);
        }
    }
}
