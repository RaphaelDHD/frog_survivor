using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int Health = 100;
    public int Damage = 10;
    public GameObject BloodPrefab;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemyManager.Instance.EnnemyKilled();
        Quaternion enemyRotation = gameObject.transform.rotation;
        Quaternion bloodRotation = enemyRotation * Quaternion.Euler(0, 90, 0);
        Vector3 bloodPosition = gameObject.transform.position;
        bloodPosition.y -= 1.4f;
        Instantiate(BloodPrefab, bloodPosition, bloodRotation);
        Destroy(gameObject);
    }


}
