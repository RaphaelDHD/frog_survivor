using UnityEngine;

public class DestroyOnDistance : MonoBehaviour
{
    private GameObject player = null;
    public float destroyDistance = 600f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}