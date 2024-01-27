using System.Collections.Generic;
using UnityEngine;

public class GenerateOnTrigger : MonoBehaviour
{
    public GameObject groundPrefab;
    
    private List<Vector3> instantiatedPositions = new List<Vector3>();
    void Start()
    {
        Vector3[] positions = new Vector3[]
        {
            new Vector3(300f, 0f, 0f),
            new Vector3(-300f, 0f, 0f),
            new Vector3(0f, 0f, 300f),
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 0f, -300f),
            new Vector3(300f, 0f, 300f),
            new Vector3(-300f, 0f, 300f),
            new Vector3(300f, 0f, -300f),
            new Vector3(-300f, 0f, -300f)
        };

        foreach (Vector3 pos in positions)
        {
            InstantiateGroundPrefab(pos);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground"){
       
            Vector3 groundPosition = other.gameObject.transform.position;

            InstantiateGroundPrefab(groundPosition + new Vector3(300f, 0f, 0f));  // Création à +300 en X
            InstantiateGroundPrefab(groundPosition + new Vector3(300f, 0f, 300f)); // Création à +300 en X et +300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(300f, 0f, -300f)); // Création à +300 en X et -300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(-300f, 0f, 0f)); // Création à -300 en X
            InstantiateGroundPrefab(groundPosition + new Vector3(-300f, 0f, 300f)); // Création à -300 en X et +300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(-300f, 0f, -300f)); // Création à -300 en X et -300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(0f, 0f, 300f));  // Création à +300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(0f, 0f, -300f)); // Création à -300 en Z
        }
    }

    void InstantiateGroundPrefab(Vector3 position)
    {
        if (!instantiatedPositions.Contains(position))
        {
            instantiatedPositions.Add(position);
            Instantiate(groundPrefab, position, Quaternion.identity);
        }
    }
    
}
