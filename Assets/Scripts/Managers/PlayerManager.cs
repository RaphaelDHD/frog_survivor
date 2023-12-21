using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player = null;

    public GameObject attackPrefab = null;
    public GameObject attackCritickPrefab = null;

    public Animator animator = null;

    public int experience = 0;
    private int experienceToReachNextLevel = 100;
    public int level = 1;
    public int maxLevel = 100;

    public int health = 100;
    public int maxHealth = 100;

    public int damage = 1;
    public float attackSpeed = 1f;
    public int attackAngle = 45;

    public float range = 100f;

    public int criticalChance = 1;

    public int knockback = 1;

    public int defense = 0;

    public int speed = 20;

    // make singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }



    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void takeDamage()
    {
        health -= 10;
        setAnimation("IsTakingDamage", true);

        if (health <= 0)
        {
            Debug.Log("Player died!");
            Destroy(player);
        }

        StartCoroutine(ResetTakingDamageAnimation());
    }

    private IEnumerator ResetTakingDamageAnimation()
    {
        yield return new WaitForSeconds(1.0f); 
        setAnimation("IsTakingDamage", false);
    }

    public void gainExperience(int exp)
    {
        experience += exp;

        if (experience >= experienceToReachNextLevel)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        if (level < maxLevel)
        {
            level++;
            experience = 0;
            experienceToReachNextLevel = experienceToReachNextLevel * 2;
        }
    }

    public void spawnAttackPrefab(GameObject enemy)
    {
        if (enemy == null || attackPrefab == null)
        {
            return; // Ensure enemy and attackPrefab are valid
        }

        Vector3 direction = enemy.transform.position - player.transform.position;
        direction.y = 1f; // Keep the attack level with the ground, remove if not needed
        Vector3 position = player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        GameObject attack = Instantiate(attackPrefab, position, rotation);
        Vector3 offset = direction.normalized * range * 0.5f; // Adjust the multiplier based on your preference
        attack.transform.position = position + offset;
        float attackSize = range * 0.25f; // Adjust the multiplier based on your preference
        attack.transform.localScale = new Vector3(attackSize, 1, attackSize);
    }



    public void setAnimation(string animationName, bool value)
    {
        animator.SetBool(animationName, value);
    }


}
