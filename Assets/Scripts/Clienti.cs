using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clienti : MonoBehaviour
{
    public bool orderDone;
    [SerializeField] private ScriptableObjects[] cocktailPrefabs;
    [SerializeField] int cocktailIndex;

    [SerializeField] int requiredVodka;
    [SerializeField] int requiredGin;
    [SerializeField] int requiredLemonSoda;
    [SerializeField] int requiredTonic;
    [SerializeField] int requiredLemonSlice;

    private void Awake()
    {
        cocktailIndex = Random.Range(0, cocktailPrefabs.Length);
        requiredVodka = cocktailPrefabs[cocktailIndex].vodka;
        requiredGin = cocktailPrefabs[cocktailIndex].gin;
        requiredLemonSoda = cocktailPrefabs[cocktailIndex].lemonSoda;
        requiredTonic = cocktailPrefabs[cocktailIndex].tonica;
        requiredLemonSlice = cocktailPrefabs[cocktailIndex].limeSlice;
    }
}
