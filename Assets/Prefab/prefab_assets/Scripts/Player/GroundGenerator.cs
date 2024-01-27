using System.Collections.Generic;
using UnityEngine;

public class GroundPlacer : MonoBehaviour
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
            new Vector3(0f, 0f, -300f),
            new Vector3(300f, 0f, 300f),
            new Vector3(-300f, 0f, 300f),
            new Vector3(300f, 0f, -300f),
            new Vector3(-300f, 0f, -300f)
        };

        foreach (Vector3 pos in positions)
        {
            Instantiate(groundPrefab, pos, Quaternion.identity);
        }
    }
    void Update()
    {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    {
        Debug.Log("raycast hit");
        Debug.Log($"{hit.transform.gameObject}");
        if (hit.transform.gameObject.CompareTag("Ground"))
        {

            Vector3 groundPosition = hit.transform.position;
            Debug.Log($"{groundPosition}");

            InstantiateGroundPrefab(groundPosition + new Vector3(300f, 0f, 0f));  // Création à +300 en X
            InstantiateGroundPrefab(groundPosition + new Vector3(-300f, 0f, 0f)); // Création à -300 en X
            InstantiateGroundPrefab(groundPosition + new Vector3(0f, 0f, 300f));  // Création à +300 en Z
            InstantiateGroundPrefab(groundPosition + new Vector3(0f, 0f, -300f)); // Création à -300 en Z
        }
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
