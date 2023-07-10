using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerClienti : MonoBehaviour
{
    [Header("Customer(s) to spawn")]
    [SerializeField] GameObject enemyPrefab;
    [Header("Timers")]
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float customersSpawnInterval = 1f;

    [Header("List Spawned Customer(s)")]
    public List<GameObject> enemies = new List<GameObject>();//so che non è necessaria ma ho intenzione di aggiungere più tipi di clienti per l'esame finale


    [Header("UI Reference")]
    [SerializeField] TextMeshProUGUI roundText;

    private int currentRound = 1;
    private int enemiesToSpawn = 1;

    private bool isSpawning = false;


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

        if (enemies.Count > 0)
        {
            enemies[0].GetComponent<NavMeshAgent>().avoidancePriority = 1;
        }
        roundText.text = "Round : " + currentRound;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitUntil(() => TimeManager.instance.day && enemies.Count == 0);

            isSpawning = true;

            //yield return new WaitUntil(() => enemies.Count == 0);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemies.Add(newEnemy);
                yield return new WaitForSeconds(customersSpawnInterval);
            }

            currentRound++;
            //enemiesToSpawn++;

            isSpawning = false;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
