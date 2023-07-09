using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClienti : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public float spawnInterval = 5f; 
    private int currentRound = 1; 
    private int enemiesToSpawn = 1;  
    public List<GameObject> enemies = new List<GameObject>(); 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (enemies.Count > 0 && enemies[0].GetComponent<Clienti>().orderDone)
        {
            
            enemies.RemoveAt(0);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {

            yield return new WaitUntil(() => enemies.Count == 0 && TimeManager.instance.day);

            
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    enemies.Add(newEnemy);
                    yield return new WaitForSeconds(1f);  
                }
            


            currentRound++;
            enemiesToSpawn++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
