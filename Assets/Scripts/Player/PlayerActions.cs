using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody player = null;
    private bool isCooldownActive = false;

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
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            PlayerManager.Instance.setAnimation("IsAttacking", true);
            yield return new WaitForSeconds(0.40f); // Adjust the duration as needed
            attack();

            yield return new WaitForSeconds(0.60f); // Adjust the duration as needed

            // Set the "IsAttacking" animation to false
            PlayerManager.Instance.setAnimation("IsAttacking", false);

            // Wait for the remaining attackSpeed duration
            yield return new WaitForSeconds(PlayerManager.Instance.attackSpeed - 0.1f);
        }
    }

    private void attack()
    {
        if (PlayerManager.Instance.attackPrefab != null)
        {
            PlayerManager.Instance.spawnAttackPrefab();
        }
    }
}
