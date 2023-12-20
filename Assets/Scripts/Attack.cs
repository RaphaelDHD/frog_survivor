using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attack : MonoBehaviour
{
    private List<Enemy> collideEnemies = new List<Enemy>();

    private void Start()
    {
        Destroy(gameObject, 1f) ;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && !collideEnemies.Contains(enemy))
        {
            collideEnemies.Add(enemy);
            enemy.TakeDamage(10);
        }
    }
}
