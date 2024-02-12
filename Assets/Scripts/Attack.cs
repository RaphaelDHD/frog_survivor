using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attack : MonoBehaviour
{
    private List<Enemy> collideEnemies = new List<Enemy>();

    public bool isCritical = false;

    private void Start()
    {
        Destroy(gameObject, 0.3f) ;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && !collideEnemies.Contains(enemy))
        {
            collideEnemies.Add(enemy);
            if (isCritical)
            {
                enemy.TakeDamage(PlayerManager.Instance.damage * 2);
            }
            else
            {
                enemy.TakeDamage(PlayerManager.Instance.damage);
            }
        }
    }
}
