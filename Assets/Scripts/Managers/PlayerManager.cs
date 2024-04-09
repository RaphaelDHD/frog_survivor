using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    public GameObject player = null;

    public GameObject attackPrefab = null;
    public GameObject attackCritickPrefab = null;

    public Image healthContainer;
    public Image healthSlider;

    public Image ExperienceContainer;
    public Image ExperienceSlider;
    public TextMeshProUGUI ExperienceText;

    public GameObject deadMenu;

    public Animator animator = null;

    public int experience = 0;
    private int experienceToReachNextLevel = 100;
    public int level = 1;
    public int maxLevel = 100;

    public int health = 100;
    public int maxHealth;

    public int damage = 10;
    public float attackSpeed = 2.5f;
    public int attackAngle = 45;
    
    public float range = 100f;

    public int criticalChance = 1;

    public int knockback = 1;


    public float speed = 40f;

    // effect assets
    public GameObject healEffect;


    // sound effect
    public AudioClip levelUpSound;
    public AudioClip[] attackSounds;

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

    public void Start()
    {
        healthContainer.rectTransform.sizeDelta = new Vector2(maxHealth, healthContainer.rectTransform.sizeDelta.y);
        healthSlider.rectTransform.sizeDelta = new Vector2(maxHealth, healthSlider.rectTransform.sizeDelta.y);
        healthSlider.type = Image.Type.Filled;
        healthSlider.fillAmount = ((float)health / maxHealth);

        ExperienceContainer.rectTransform.sizeDelta = new Vector2(experienceToReachNextLevel * 5, ExperienceContainer.rectTransform.sizeDelta.y);
        ExperienceSlider.rectTransform.sizeDelta = new Vector2(experienceToReachNextLevel * 5, ExperienceSlider.rectTransform.sizeDelta.y);
        ExperienceSlider.type = Image.Type.Filled;
        ExperienceSlider.fillAmount = ((float)experience / experienceToReachNextLevel);

    }


    public void takeDamage()
    {
        health -= 10;
        setAnimation("IsTakingDamage", true);

        healthSlider.fillAmount = ((float)health / maxHealth);
        

        if (health <= 0)
        {
            Death();
        }

        StartCoroutine(ResetTakingDamageAnimation());
    }

    public void recoverLife(int percentage)
    {
        Debug.Log(health);
        Debug.Log(percentage);
        health += (int)((float)maxHealth * (percentage / 100f));
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        Debug.Log(health);
        healthSlider.fillAmount = ((float)health / maxHealth);
        healEffect.SetActive(true);
        StartCoroutine(DisableHealEffect());
    }

    private IEnumerator DisableHealEffect()
    {
        yield return new WaitForSeconds(4.0f);
        healEffect.SetActive(false);
    }

    public void Death()
    {
        EnemyManager.Instance.killAllEnemy();
        deadMenu.SetActive(true);
        Destroy(player);
    }


    private IEnumerator ResetTakingDamageAnimation()
    {
        yield return new WaitForSeconds(1.0f); 
        setAnimation("IsTakingDamage", false);
    }

    public void gainExperience(int exp)
    {
        experience += exp;
        ExperienceSlider.fillAmount = ((float)experience / experienceToReachNextLevel);

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
            experienceToReachNextLevel = (int)(experienceToReachNextLevel * 1.25f);
            ExperienceSlider.fillAmount = ((float)experience / experienceToReachNextLevel);

            ExperienceText.text = "Level : " + level;

            AugmentManager.Instance.playerLevelledUp();
            SoundManager.Instance.PlaySound(levelUpSound, transform);

        }
    }

    public void spawnAttackPrefab(GameObject enemy)
    {
        if (enemy == null || attackPrefab == null)
        {
            return; // Ensure enemy and attackPrefab are valid
        }

        Vector3 direction = enemy.transform.position - player.transform.position;
        direction.y = 1f;
        Vector3 position = player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        GameObject attack = Instantiate(attackPrefab, position, rotation);
        Vector3 offset = direction.normalized * range * 0.5f; // Adjust the multiplier based on your preference
        attack.transform.position = position + offset;
        float attackSize = range * 0.25f; // Adjust the multiplier based on your preference
        attack.transform.localScale = new Vector3(attackSize, 1, attackSize);
    }

    public void spawnCritAttackPrefab(GameObject enemy)
    {
        if (enemy == null || attackPrefab == null)
        {
            return; // Ensure enemy and attackPrefab are valid
        }

        Vector3 direction = enemy.transform.position - player.transform.position;
        direction.y = 1f; // Keep the attack level with the ground, remove if not needed
        Vector3 position = player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);

        GameObject attack = Instantiate(attackCritickPrefab, position, rotation);
        Vector3 offset = direction.normalized * range * 0.5f; // Adjust the multiplier based on your preference
        attack.transform.position = position + offset;
        float attackSize = range * 0.25f; // Adjust the multiplier based on your preference
        attack.transform.localScale = new Vector3(attackSize, 1, attackSize);
    }




    public void setAnimation(string animationName, bool value)
    {
        animator.SetBool(animationName, value);
    }

    internal void gainMaxHealth(int value)
    {
        maxHealth += value;
        healthContainer.rectTransform.sizeDelta = new Vector2(maxHealth, healthContainer.rectTransform.sizeDelta.y);
        healthSlider.rectTransform.sizeDelta = new Vector2(maxHealth, healthSlider.rectTransform.sizeDelta.y);
        healthSlider.fillAmount = ((float)health / maxHealth);
    }

    public void playAttackSound(Transform _transform)
    {
        AudioClip clip = attackSounds[Random.Range(0, attackSounds.Length)];
        SoundManager.Instance.PlaySound(clip, _transform);
    }


}
