using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject spawner = null;
    private BoxCollider spawnBounds = null;
    public Vector2 spawnTime = Vector2.zero;
    public float spawnHeight = 1.5f;
    public GameObject spawnObject = null;
    public GameObject player = null;
    public int maxEnemies = 5;

    private int enemyCount = 0;

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


        while (enemyCount >= maxEnemies)
        {
            yield return new WaitForSeconds(1.0f);
        }

        Vector3 spawnPos = GetRandomEdgePosition();
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
        enemyCount++;
        StartCoroutine(SpawnObject());
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


    public void EnnemyKilled()
    {
        enemyCount--;
    }

}
