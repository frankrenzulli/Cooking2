using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SpawnerClienti : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float delayBetweenEnemies = 1f;

    [SerializeField] private int round = 1;
    [SerializeField] private int enemyCount = 1;
    [SerializeField] private bool isSpawning = false;

    public List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitUntil(() => transform.childCount == 0 && TimeManager.instance.day);

            Debug.Log($"Round {round} - Spawning {enemyCount} enemies");

            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemies.Add(enemy);
                enemy.transform.parent = transform;

                // Aggiungi la chiamata a CheckEnemiesStatus() dopo l'istanziazione di ogni nemico
                CheckEnemiesStatus();

                yield return new WaitForSeconds(delayBetweenEnemies);
            }

            round++;
            enemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void CheckEnemiesStatus()
    {
        // Itera attraverso la lista degli enemies all'indietro
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = enemies[i];

            // Ottieni lo script "Clienti" attaccato all'oggetto
            Clienti clientiScript = enemy.GetComponent<Clienti>();

            // Verifica la variabile desiderata
            if (clientiScript != null && clientiScript.orderDone)
            {
                // La variabile � uguale a 0, rimuovi l'oggetto dalla lista e distruggilo
                enemies.RemoveAt(0);
                Destroy(enemy);
                
            }
        }
    }
}