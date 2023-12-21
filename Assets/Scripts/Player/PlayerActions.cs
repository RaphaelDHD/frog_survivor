using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody player = null;
    public BoxCollider attackZone = null;

    private bool isCooldownActive = false;
    private bool canAttack = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && !isCooldownActive)
        {
            PlayerManager.Instance.takeDamage();
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldownActive = true;

        yield return new WaitForSeconds(0.75f);

        isCooldownActive = false;
    }

    private void Start()
    {
        if (attackZone != null)
        {
            float range = PlayerManager.Instance.range;
            attackZone.transform.localScale = new Vector3(range, 5, range);
        }
        StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            // Check if an enemy is in the attack zone
            if (canAttack && IsEnemyInAttackZone())
            {
                PlayerManager.Instance.setAnimation("IsAttacking", true);
                yield return new WaitForSeconds(0.40f);
                attack(getNearestEnemy());
                yield return new WaitForSeconds(0.60f);
                PlayerManager.Instance.setAnimation("IsAttacking", false);

                // Disable further attacks for a cooldown period
                canAttack = false;
                yield return new WaitForSeconds(1.0f);
                canAttack = true;
            }

            // Wait before checking for attacks again
            yield return new WaitForSeconds(0.1f);
        }   
    }

    private void attack(GameObject enemy)
    {
        if (PlayerManager.Instance.attackPrefab != null)
        {
            PlayerManager.Instance.spawnAttackPrefab(enemy);
        }
    }

    private bool IsEnemyInAttackZone()
    {
        Collider[] hitColliders = Physics.OverlapBox(attackZone.transform.position, attackZone.transform.localScale / 2, Quaternion.identity);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    private GameObject getNearestEnemy()
    {
        // get the nearest enemy to the player
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }



}
