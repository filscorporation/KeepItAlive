using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    public class SpawnManager : MonoBehaviour
    {
        private List<Spawner> spawners;

        public List<GameObject> EnemiesPrefabs;
        public float SpawnTimeout = 4F;
        private float spawnTimer = 4F;
        public float ScaleFactor = 0.05F;

        public void Start()
        {
            spawnTimer = SpawnTimeout;
            spawners = FindObjectsOfType<Spawner>().ToList();
        }

        public void Update()
        {
            spawnTimer = Mathf.Max(0, spawnTimer - Time.deltaTime);
            SpawnTimeout = Mathf.Max(0.5F, SpawnTimeout - ScaleFactor * Time.deltaTime);
            if (Mathf.Abs(spawnTimer) < Mathf.Epsilon)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            spawners[Random.Range(0, spawners.Count)].Spawn(EnemiesPrefabs[Random.Range(0, EnemiesPrefabs.Count)]);
            spawnTimer = SpawnTimeout;
        }
    }
}
