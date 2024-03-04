using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public float timer = 0.0f;
    void Start()
    {
        //enemy  = GetComponent<GameObject>();
        /*float x = Random.Range(24, 51);
        float y = 15.5f;
        float z = Random.Range(4, 99);

        Vector3 spawnPoint  = new Vector3(x,y,z);

        GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);*/
        
        //Spawn(Random.Range(24, 51), 15.5f, Random.Range(4, 99), enemy);
        timer = Time.time;
        
        Debug.Log("class:EnemySpawn --> Start()");
        
    }

    // Update is called once per frames
    void Update()
    {
        if (enemy != null && Time.time - timer > 20) {
            Spawn(Random.Range(24, 51), 15.5f, Random.Range(4, 99), enemy);
            timer = Time.time;
        }
    }
    void Spawn(float x, float y, float z, GameObject obj)
    {
        Vector3 spawnPoint  = new Vector3(x, y, z);
        GameObject newEnemy = Instantiate(obj, spawnPoint, Quaternion.identity) as GameObject;
    }
}
