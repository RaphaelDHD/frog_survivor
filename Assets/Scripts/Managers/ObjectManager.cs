using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Singleton
    private static ObjectManager _instance;
    public static ObjectManager instance { get { return _instance; } }

    public GameObject lifePrefab = null;

    [Header("Spawn Time Range")]
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 10.0f;

    private const int minNegativeDistance = -80;
    private const int maxNegativeDistance = -25;
    private const int minPositiveDistance = 25;
    private const int maxPositiveDistance = 80;

    private float positionX = 0.0f;
    private float positionZ = 0.0f;


    void Start()
    {
        if (lifePrefab != null)
        {
            LifeAppears(); // Initially call LifeAppears
        }
    }

    void LifeAppears()
    {
        GameObject playerObject = GameObject.Find("Player");

        // Choix de la position X positive ou négative
        if (Random.Range(0, 2) == 0)
        {
            positionX = Random.Range(minNegativeDistance, maxNegativeDistance);
        }
        else
        {
            positionX = Random.Range(minPositiveDistance, maxPositiveDistance);
        }

        // Choix de la position Z positive ou négative
        if (Random.Range(0, 2) == 0)
        {
            positionZ = Random.Range(minNegativeDistance, maxNegativeDistance);
        }
        else
        {
            positionZ = Random.Range(minPositiveDistance, maxPositiveDistance);
        }

        // Définition de la position d'apparition
        Vector3 randomPositionAppearance = new Vector3(
            playerObject.transform.position.x + positionX,
            9.0f,
            playerObject.transform.position.z + positionZ
        );

        Instantiate(lifePrefab, randomPositionAppearance, Quaternion.identity);

        // Set a new random spawn time for the next call
        float randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("LifeAppears", randomSpawnTime);
    }
}
