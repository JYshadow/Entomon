using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Public")]
    public Wave[] waves;
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public GameObject[] enemyPool;
    }

    GameObject enemyContainer;
    Wave currentWave;
    Vector3 offset;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    private void Start()
    {

        StartCoroutine(NextWave(Random.Range(2f, 4f)));
        enemyContainer = GameObject.FindGameObjectWithTag("EnemiesContainer");
    }

    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)   //if there are still enemies to spawn and its ready to spawn, spawn the enemy
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + Random.Range(1f, 3f);  //if spawntime is reached, add a random time on top of it
            offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));    //offset the spawning radius by declaring a Vector3 range
            GameObject spawnedEnemy = Instantiate(currentWave.enemyPool[Random.Range(0, currentWave.enemyPool.Length)], transform.position + offset, transform.rotation, enemyContainer.transform) as GameObject; //Instantiate the gameobject as a new version of the gameobject that belongs to this spawner specifically
            spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
            spawnedEnemy.GetComponent<LivingEntity>().OnDeath += EnemyDied;     //give the spawned enemy the EnemyDied fuction
        }
    }

    private void EnemyDied()
    {
        transform.GetComponentInParent<AreaCounter>().totalEnemiesInArea--;
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive == 0)
        {
            StartCoroutine(NextWave(Random.Range(2f, 4f)));
        }
    }

    public IEnumerator NextWave(float delay)
    {
        yield return new WaitForSeconds(delay);    //wait a few seconds before next wave starts
        currentWaveNumber++;    //increase wave number
        if (currentWaveNumber - 1 < waves.Length)   //if there are still waves left
        {
            currentWave = waves[currentWaveNumber - 1]; //set currentWave

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }
}