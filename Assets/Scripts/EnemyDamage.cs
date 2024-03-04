using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class EnemyDamage : MonoBehaviour
{

    public GameObject enemy;
    public Collider bullet;
    private float health;
    public float maxHealth = 100f;

    public bool isHit = false;

    public UnityEngine.Object objectToDestroy;

    public ParticleSystem explosion;
    public TextMeshProUGUI killCountDisplay;
    public float killCount;
    //public GameObject hitMarker;
    // Start is called before the first frame update
    void Start()
    {
        enemy  = GetComponent<GameObject>();
        health = maxHealth;
        string text = killCountDisplay.text;
        killCount = float.Parse(text.Substring(14));
        
        Debug.Log(text+"");
        //Debug.Log("Enemy Health: "+health);
        killCountDisplay.SetText("Eliminations: "+killCount);
        //hitMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isHit)
        {
            explosion.Play();
        }*/
        /*if(health <= 0)
        {
            Death();
        }*/
        
        //Debug.Log("class: EnemyDamage --> Update()");
        if(isHit)
        {
            isHit = false;
        }



    }

    void Death()
    {
        //Debug.Log("Explode time : " + explosion.main.duration);
        explosion.Play();
        killCount++;
        killCountDisplay.SetText("Eliminations: "+killCount);
        //if(healthBar.fillAmount<1)
         //   healthBar.fillAmount+=.1;
        Destroy(this.objectToDestroy, explosion.main.duration/10);
        //Destroy(enemy, explosion.main.duration/10);
    }

    public void takeDamage(int damage)
    {
        isHit = true;
        //hitMarker.SetActive(true);
        health-=damage;
        Debug.Log(health + " : class:EnemyDamage --> takeDamage()");
        if(health <= 0)
        {
            Death();
        }
        //explosion.Play();
    }
    public float getHealth()
    {
        return health;
    }
    public bool isEnemyHit()
    {
        return isHit;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(health>0) {
            Debug.Log("Enemy Health: "+health);
            health -= 20;
        }   
        else if (health <= 0 ) {
            Death();
            Debug.Log("Enemy Health: "+health);
            Debug.Log("Enemy Dead");
        }

        //explosion.Play();
        isHit = true;
    }*/
}
