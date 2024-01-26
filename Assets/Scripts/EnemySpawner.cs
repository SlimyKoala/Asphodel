using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform spawnPosition;
    [SerializeField] GameObject[] enemiesToSpawn;
    
    private float lastSpawnMoment;
    private float spawnDelayTime;

    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 5f;

    private int childCount;

    void Start()
    {

        spawnDelayTime = Random.Range(minSpawnTime, maxSpawnTime);
        lastSpawnMoment = Time.time;
        childCount = transform.childCount;
    }


    void Update()
    {
        if(Time.time - lastSpawnMoment > spawnDelayTime)
        {
            int randomSpawnNumber = Random.Range(0, childCount);

            spawnPosition = transform.GetChild(randomSpawnNumber);
            SpawnEnemy();
            spawnDelayTime = Random.Range(minSpawnTime, maxSpawnTime);
            lastSpawnMoment = Time.time;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        Instantiate(enemyToSpawn, spawnPosition.position, spawnPosition.rotation);
    }
}
