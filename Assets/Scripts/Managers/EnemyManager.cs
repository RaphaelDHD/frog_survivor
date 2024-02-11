using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject spawner = null;
    private BoxCollider spawnBounds = null;
    public Vector2 spawnTime = Vector2.zero;
    public float spawnHeight = 1.5f;
    public GameObject player = null;

    // Wave system
    public List<EnemyWave> enemies = new List<EnemyWave>();

    private int currWave = 1;
    private int waveValue = 10;
    private int enemyCount = 0;
    private bool isWaveInProgress = false;

    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }
    



    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        spawnBounds = spawner.GetComponent<BoxCollider>();
        StartCoroutine(SpawnObject());
        StartCoroutine(ManageWaves());
        
    }

    private void Update()
    {
        spawner.transform.position = player.transform.position;
    }

    private void newWave()
    {
        currWave++;
        waveValue = currWave * 10;
        StartCoroutine(SpawnObject());
    }

    private IEnumerator ManageWaves()
    {
        while (true) // Infinite loop to constantly check the end of the wave
        {
            yield return new WaitForSeconds(3f); // Adjust the interval based on your needs

            if (!isWaveInProgress && enemyCount == 0 && waveValue <= 0)
            {
                // End of the wave condition is met
                newWave();
            }
        }
    }

    private IEnumerator SpawnObject()
    {
        isWaveInProgress = true;
        float randSpawnTime = Random.Range(spawnTime.x, spawnTime.y);
        yield return new WaitForSeconds(randSpawnTime);

        Vector3 spawnPos = GetRandomEdgePosition();
        GameObject enemy = getEnemyToSpawn().enemyPrefab;
        Instantiate(enemy, spawnPos, Quaternion.identity);
        enemyCount++;
        waveValue -= getEnemyToSpawn().cost;

        if (waveValue > 0)
        {
            StartCoroutine(SpawnObject());
        } else
        {
            isWaveInProgress = false;
        }
    }




    private Vector3 GetRandomEdgePosition()
    {
        int edgeIndex = Random.Range(0, 4); // 0: left, 1: right, 2: top, 3: bottom
        float xSpawn = 0f, zSpawn = 0f;

        switch (edgeIndex)
        {
            case 0: // left
                xSpawn = spawnBounds.bounds.min.x;
                zSpawn = Random.Range(spawnBounds.bounds.min.z, spawnBounds.bounds.max.z);
                break;
            case 1: // right
                xSpawn = spawnBounds.bounds.max.x;
                zSpawn = Random.Range(spawnBounds.bounds.min.z, spawnBounds.bounds.max.z);
                break;
            case 2: // top
                xSpawn = Random.Range(spawnBounds.bounds.min.x, spawnBounds.bounds.max.x);
                zSpawn = spawnBounds.bounds.max.z;
                break;
            case 3: // bottom
                xSpawn = Random.Range(spawnBounds.bounds.min.x, spawnBounds.bounds.max.x);
                zSpawn = spawnBounds.bounds.min.z;
                break;
        }

        return new Vector3(xSpawn, spawnHeight, zSpawn);
    }

    public EnemyWave getEnemyToSpawn()
    {
        List<EnemyWave> genereableEnemies = new List<EnemyWave>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (waveValue - enemies[i].cost >= 0)
            {
                genereableEnemies.Add(enemies[i]);
            }
        }
        int randomIndex = Random.Range(0, genereableEnemies.Count);
        return genereableEnemies[randomIndex];
    } 

    public void EnnemyKilled()
    {
        enemyCount--;
        PlayerManager.Instance.gainExperience(enemyCount * 10);
        if (enemyCount == 0 && waveValue <= 0)
        {
            isWaveInProgress = false; // Set the flag to false when the wave ends
        }
    }

}

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int cost;
}