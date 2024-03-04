using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDamage : MonoBehaviour, IHeal
{
    public GameObject player;
    public Collider fireball;
    public float health;
    public float maxHealth = 100f;

    public bool isHit = false;
   // public TextMeshProUGUI healthDisplay;

   // public Object objectToDestroy;
    public GameObject GameOverScreen;
    public Image healthBar;
    public TextMeshProUGUI healthText;

    public void Heal() {
        Debug.Log("Picked up Meds");
        if(health<100)
        {
            if(health+20>100)
                health = 100;
            else
                health+=20;
            healthBar.fillAmount = health / 100f;
            healthText.SetText(health+"");
        }
    }

    private void Awake()
    {
        //when game starts, make not visible
        GameOverScreen.SetActive(false); 
    }
    void Start()
    {
        player = GetComponent<GameObject>();
        health = maxHealth;
        healthBar.fillAmount = 1;
        //healthDisplay.SetText(maxHealth+"");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void Death()
    {
        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
    }
    void takeDamage(int damage)
    {
        health-=damage;
        if(health > 0)
        {
            healthBar.fillAmount = health / 100f;
            healthText.SetText(health+"");
        }   
        Debug.Log(health + " : class:PlayerDamage --> takeDamage()");
        /*if(healthDisplay != null)
        {
            if(health<0) {
                healthDisplay.SetText("0");
            }
            else
                healthDisplay.SetText(health+"");
        }*/
        if(health <= 0)
        {
            healthBar.fillAmount = 0;
            healthText.SetText("0");
            Death();
        }
    }
    public float getHealth()
    {
        return health;
    }
    private void OnTriggerEnter(Collider other)
    {
        isHit = true;
        //Destroy(other);
        if(health>0) {
            Debug.Log("Player Health: "+health);
            takeDamage(10);
        }   
        else if (health <= 0 ) {
            Death();
            Debug.Log("Player Health: "+health);
            Debug.Log("Player Dead");
        }

        isHit = false;
    }
}
