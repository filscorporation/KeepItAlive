using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }

        private List<Spawner> spawners;
        private List<Spawner> debuffSpawners;

        public List<GameObject> EnemiesPrefabs;
        public List<GameObject> DebuffPrefabs;
        public List<GameObject> PowerUpPrefabs;
        public GameObject BombPrefab;
        public GameObject HarderEnemyPrefabs;
        public float HarderEnemiesAddTime = 10F;
        private bool harderEnemySpawned = false;
        private float harderEnemiesAddTimer;
        public float SpawnTimeout = 4F;
        private float spawnTimer = 4F;
        public float ScaleFactor = 0.05F;
        public float DebuffSpawnTimeout = 10F;
        private float debuffSpawnTimer = 10F;
        public float BombSpawnTimeout = 20F;
        private float bombSpawnTimer = 20F;
        
        public void Awake()
        {
            Instance = FindObjectOfType<SpawnManager>();
        }

        public void Start()
        {
            spawnTimer = SpawnTimeout;
            spawners = FindObjectsOfType<Spawner>().Where(s => s.SpawnerType == SpawnerType.Enemy).ToList();
            debuffSpawnTimer = DebuffSpawnTimeout;
            debuffSpawners = FindObjectsOfType<Spawner>().Where(s => s.SpawnerType == SpawnerType.Debuff).ToList();
            harderEnemiesAddTimer = HarderEnemiesAddTime;
            bombSpawnTimer = BombSpawnTimeout;
        }

        public void Update()
        {
            spawnTimer = Mathf.Max(0, spawnTimer - Time.deltaTime);
            SpawnTimeout = Mathf.Max(1.0F, SpawnTimeout - ScaleFactor * Time.deltaTime);
            if (Mathf.Abs(spawnTimer) < Mathf.Epsilon)
            {
                Spawn();
            }

            debuffSpawnTimer = Mathf.Max(0, debuffSpawnTimer - Time.deltaTime);
            if (Mathf.Abs(debuffSpawnTimer) < Mathf.Epsilon)
            {
                SpawnDebuff();
            }

            if (!harderEnemySpawned)
            {
                harderEnemiesAddTimer = Mathf.Max(0, harderEnemiesAddTimer - Time.deltaTime);
                if (Mathf.Abs(harderEnemiesAddTimer) < Mathf.Epsilon)
                {
                    harderEnemySpawned = true;
                    EnemiesPrefabs.Add(HarderEnemyPrefabs);
                    harderEnemiesAddTimer = 20F;
                }
            }

            bombSpawnTimer = Mathf.Max(0, bombSpawnTimer - Time.deltaTime);
            if (Mathf.Abs(bombSpawnTimer) < Mathf.Epsilon)
            {
                SpawnBomb();
                bombSpawnTimer = BombSpawnTimeout;
            }
        }

        private void Spawn()
        {
            spawners[Random.Range(0, spawners.Count)].Spawn(EnemiesPrefabs[Random.Range(0, EnemiesPrefabs.Count)]);
            spawnTimer = SpawnTimeout;
        }

        private void SpawnDebuff()
        {
            debuffSpawners[Random.Range(0, debuffSpawners.Count)].Spawn(DebuffPrefabs[Random.Range(0, DebuffPrefabs.Count)]);
            debuffSpawnTimer = DebuffSpawnTimeout;
        }

        public void SpawnPowerUp(float probability)
        {
            if (Random.Range(0, 1F) < probability)
            {
                debuffSpawners[Random.Range(0, debuffSpawners.Count)].Spawn(PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Count)]);
            }
        }

        public void SpawnBomb()
        {
            debuffSpawners[Random.Range(0, debuffSpawners.Count)].Spawn(BombPrefab);
        }
    }
}
