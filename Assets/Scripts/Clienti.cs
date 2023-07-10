using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Clienti : MonoBehaviour
{
    [Header("Spots")]
    [SerializeField] Transform exit;

    [Header("Sprites")]

    [SerializeField] Image Face;
    [SerializeField] Image firstIngredientSprite;
    [SerializeField] Image secondIngredientSprite;
    [SerializeField] Sprite DrunkFace;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI firstIngredientText;
    [SerializeField] TextMeshProUGUI secondIngredientText;
    [SerializeField] TextMeshProUGUI thirdIngredientText;
    [SerializeField] TextMeshProUGUI cocktailName;


    public bool orderDone;

    [SerializeField] int cocktailIndex;
    [Header("References")]
    Transform Bar;
    NavMeshAgent agent;
    [SerializeField] private ScriptableObjects[] cocktailPrefabs;


    [Header("Required ingredients")]
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
        firstIngredientText.color = cocktailPrefabs[cocktailIndex].FirstIngredientColor;
        secondIngredientText.color = cocktailPrefabs[cocktailIndex].SecondIngredientColor;
        firstIngredientSprite.sprite = cocktailPrefabs[cocktailIndex].firstIngredientSprite;
        secondIngredientSprite.sprite = cocktailPrefabs[cocktailIndex].secondIngredientSprite;
        if (requiredVodka != 0)
        {
            firstIngredientText.text = requiredVodka + "x";
            
        }else firstIngredientText.text = requiredGin + "x";

        if(requiredLemonSoda != 0)
        {
            secondIngredientText.text = requiredLemonSoda + "x";
        }else secondIngredientText.text = requiredTonic + "x";

        exit = GameObject.Find("Exit").transform;
        cocktailName.text = cocktailPrefabs[cocktailIndex].CocktailName;
    }

    private void Update()
    {
        agent.SetDestination(Bar.position);

        if (orderDone)
        {
            Face.sprite = DrunkFace;
            agent.SetDestination(exit.position);
            if(Vector3.Distance(transform.position, exit.position) < 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
