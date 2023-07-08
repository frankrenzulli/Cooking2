using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clienti : MonoBehaviour
{
    public bool orderDone;
    [SerializeField] private ScriptableObjects[] cocktailPrefabs;
    [SerializeField] int cocktailIndex;

    Transform Bar;

    NavMeshAgent agent;

    public int requiredVodka;
    public int requiredGin;
    public int requiredLemonSoda;
    public int requiredTonic;
    public int requiredLemonSlice;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Bar = GameObject.Find("Bar").transform;
        cocktailIndex = Random.Range(0, cocktailPrefabs.Length);
        requiredVodka = cocktailPrefabs[cocktailIndex].vodka;
        requiredGin = cocktailPrefabs[cocktailIndex].gin;
        requiredLemonSoda = cocktailPrefabs[cocktailIndex].lemonSoda;
        requiredTonic = cocktailPrefabs[cocktailIndex].tonica;
        requiredLemonSlice = cocktailPrefabs[cocktailIndex].limeSlice;
    }

    private void Update()
    {
        agent.SetDestination(Bar.position);
    }
}
