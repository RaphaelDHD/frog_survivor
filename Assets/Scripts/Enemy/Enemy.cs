using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int Health = 0;
    public int Damage = 10;
    public GameObject BloodPrefab;
    public GameObject HitPrefab;

    public Image healthSlider;
    public Image background;
    private Camera _cam;

    public void Start()
    {
        Health = maxHealth;
        _cam = Camera.main;
        healthSlider.type = Image.Type.Filled;
    }


    public void Update()
    {
        // look at the camera
        background.transform.rotation = Quaternion.LookRotation(background.transform.position - _cam.transform.position);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Instantiate(HitPrefab, gameObject.transform.position, gameObject.transform.rotation);
        float fillAmount = ((float)Health / maxHealth);

        healthSlider.fillAmount = fillAmount;


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
