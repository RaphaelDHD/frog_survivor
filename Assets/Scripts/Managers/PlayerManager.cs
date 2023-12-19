using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player = null;


    public int experience = 0;
    //private int experienceToReachNextLevel= 100;
    public int level = 1;
    public int maxLevel = 100;

    public int health = 100;
    public int maxHealth = 100;

    public int attack = 1;
    public int attackSpeed = 1;
    public int attackAngle = 45;

    public int criticalChance = 1;

    public int knockback = 1;

    public int defense = 0;
    
    public int speed = 20;

    // make singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    


    void Start()
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

        if (health <= 0)
        {
            Debug.Log("Player died!");
            Destroy(player);
        }
    }
    

}
