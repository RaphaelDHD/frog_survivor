using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Singleton
    private static ObjectManager _instance;
    public static ObjectManager instance { get { return _instance; }} 

    public GameObject lifePrefab = null;

    private const int minNegativeDistance = -80;
    private const int maxNegativeDistance = -25;
    private const int minPositiveDistance = 25;
    private const int maxPositiveDistance = 80;

    private float positionX = 0.0f;
    private float positionZ = 0.0f;


    void Start()
    {
        if (lifePrefab is not null)
        {
            InvokeRepeating("LifeAppears", Random.Range(5, 11), Random.Range(15, 25));
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
        else {
            positionX = Random.Range(minPositiveDistance, maxPositiveDistance);
        }
        
        // Choix de la position Z positive ou négative
        if (Random.Range(0, 2) == 0)
        {
            positionZ = Random.Range(minNegativeDistance, maxNegativeDistance);
        }
        else {
            positionZ = Random.Range(minPositiveDistance, maxPositiveDistance);
        }

        // Définition de la position d'apparition
        Vector3 randomPositionAppearance = new Vector3(
            playerObject.transform.position.x + positionX,
            10.0f,
            playerObject.transform.position.z + positionZ
        );

        Instantiate(lifePrefab, randomPositionAppearance, Quaternion.identity);
    }

    

}
