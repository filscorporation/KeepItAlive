using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Spawner object that can instantiate enemies
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public GameObject SpawnEffect;

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
