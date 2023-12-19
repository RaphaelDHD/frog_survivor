using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject spawner = null;
    private BoxCollider spawnBounds = null;
    public Vector2 spawnTime = Vector2.zero;
    public float spawnHeight = 1.0f;
    public float minDistance = 5.0f;
    public GameObject spawnObject = null;

    public GameObject player = null;


    void Start()
    {
        StartCoroutine(SpawnObject());
        spawnBounds = spawner.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        spawner.transform.position = player.transform.position;
    }


    private IEnumerator SpawnObject()
    {
        float randSpawnTime = Random.Range(spawnTime.x, spawnTime.y);
        yield return new WaitForSeconds(randSpawnTime);

        bool farFromPlayer = false;
        Vector3 spawnPos = Vector3.zero;
        while (!farFromPlayer)
        {
            // Choose a random edge of the spawnBounds (left, right, top, or bottom)
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

            spawnPos = new Vector3(xSpawn, spawnHeight, zSpawn);
            farFromPlayer = Vector3.Distance(spawnPos, player.transform.position) > minDistance;

        }

        Instantiate(spawnObject, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnObject());
    }

}
