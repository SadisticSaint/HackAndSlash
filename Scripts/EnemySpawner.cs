using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemyPrefabs;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float respawnRate = 10;
    [SerializeField]
    private float initialSpawnDelay;
    [SerializeField]
    private int totalNumberToSpawn;
    [SerializeField]
    private int numberToSpawnEachTime = 1;

    private float spawnTimer;
    private int totalNumberSpawned;

    private void OnEnable()
    {
        spawnTimer = respawnRate - initialSpawnDelay;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (ShouldSpawn())
            Spawn();
    }

    private bool ShouldSpawn()
    {
        if (totalNumberSpawned >= totalNumberToSpawn && totalNumberToSpawn > 0)
            return false; //without this, it would go back into the spawn loop

        return spawnTimer >= respawnRate;
    }

    private void Spawn()
    {
        spawnTimer = 0;

        var availableSpawnPoints = spawnPoints.ToList(); //why not make spawnPoints a list?

        for (int i = 0; i < numberToSpawnEachTime; i++)
        {
            if (totalNumberSpawned >= totalNumberToSpawn && totalNumberToSpawn > 0)
                break; //break is used to end loops while return is to end functions

            Enemy prefab = ChooseRandomEnemyPrefab(); //setting a class to a function?

            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPoint(availableSpawnPoints);
                if (availableSpawnPoints.Contains(spawnPoint))
                    availableSpawnPoints.Remove(spawnPoint);

                //var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

                var enemy = prefab.Get<Enemy>(spawnPoint.position, spawnPoint.rotation);
                //same as var pool = Pool.GetPool(prefab);
                //var enemy = pool.Get<Enemy>()
                //then setting position and rotation

                totalNumberSpawned++;
            }
        }
    }

    private Transform ChooseRandomSpawnPoint(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0)
            return transform; //if there are no spawn points set up, instead of returning null, return this game object's transform
        if (availableSpawnPoints.Count == 1)
            return availableSpawnPoints[0]; //check to make sure spawnPoints = numberToSpawn? && numberToSpawnEachTime == 1 -- else numberToSpawnEachTime = 1

        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        return availableSpawnPoints[index];
    }

    private Enemy ChooseRandomEnemyPrefab()
    {
        if (enemyPrefabs.Length == 0) //change to switch statement?
            return null;
        if (enemyPrefabs.Length == 1)
            return enemyPrefabs[0];

        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[index];
    }

#if UNITY_EDITOR //compiles while outside play mode. How does this work?
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one);

        foreach (var spawnPoint in spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, 0.5f);
        }
    }
#endif
}
