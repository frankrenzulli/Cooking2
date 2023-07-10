using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BTConttroller : MonoBehaviour
{
    [Header("Fridge")]
    [SerializeField] Transform VodkaFridge;
    [SerializeField] Transform GinFridge;
    [SerializeField] Transform LemonSodaFridge;
    [SerializeField] Transform TonicFridge;
    [SerializeField] Transform LemonSliceFridge;
    [SerializeField] Transform Bar;

    [Header("Ingredients Stocked")]
    [SerializeField] int vodkaStocked;
    [SerializeField] int ginStocked;
    [SerializeField] int lemonsodaStocked;
    [SerializeField] int tonicStocked;
    [SerializeField] int lemonSliceStocked;


    [Header("Ingredients Spot")]
    [SerializeField]
    Transform[] ingredientsSpot;

    [Header("Bottles Prefabs")]
    [SerializeField] GameObject vodkaPrefab;
    [SerializeField] GameObject ginPrefab;
    [SerializeField] GameObject lemonsodaPrefab;
    [SerializeField] GameObject tonicPrefab;
    [SerializeField] GameObject lemonslicePrefab;

    [Header("Timers")]
    [SerializeField] float timeToReload = .5f;
    [SerializeField] float timerToReload;
    [SerializeField] float timeToPutOnBar = .5f;
    [SerializeField] float timerToPutOnBar;
    [SerializeField] float timeToMixCocktail = 1;
    [SerializeField] float timerToMixCocktail;

    [Header("References")]
    [SerializeField] SpawnerClienti spawner;
    [SerializeField] Animator anim;

    [SerializeField] Transform bed;
    [SerializeField] private int spotIndex = 0;

    [SerializeField] TextMeshProUGUI currentVodka;
    [SerializeField] TextMeshProUGUI currentGin;
    [SerializeField] TextMeshProUGUI currentLemonSoda;
    [SerializeField] TextMeshProUGUI currentTonic;
    [SerializeField] TextMeshProUGUI currentLemonSlice;

    public enum ActionState { Idle, Working };
    ActionState state = ActionState.Idle;

    BTNode.Status treeStatus = BTNode.Status.Running;

    BTRoot tree;
    NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timerToMixCocktail = timeToMixCocktail;
        timerToPutOnBar = timeToPutOnBar;
        timerToReload = timeToReload;
        agent = GetComponent<NavMeshAgent>();

        tree = new BTRoot();
        Sequence cocktailDone = new Sequence("CocktailFatto");
        Sequence VodkaReady = new Sequence("Vodka pronta");
        Sequence GinReady = new Sequence("Gin pronto");
        Sequence LemonSodaReady = new Sequence("Lemon Soda pronta");
        Sequence TonicReady = new Sequence("Tonica pronta");
        Sequence LemonSliceReady = new Sequence("Limone pronto");

        Leaf reloadVodka = new Leaf("Vodka ricaricata", ReloadVodka);
        Leaf reloadGin = new Leaf("Gin ricaricata", ReloadGin);
        Leaf reloadLemonSoda = new Leaf("LemonSoda ricaricata", ReloadLemonSoda);
        Leaf reloadTonic = new Leaf("Tonica ricaricata", ReloadTonic);
        Leaf reloadLemonSlice = new Leaf("Limone ricaricato", ReloadLemonSlice);

        Leaf vodkaOnBar = new Leaf("Vodka sul bancone", VodkaOnBar);
        Leaf ginOnBar = new Leaf("Gin sul bancone", GinOnBar);
        Leaf lemonSodaOnBar = new Leaf("LemonSoda sul bancone", LemonSodaOnBar);
        Leaf tonicOnBar = new Leaf("Tonica sul bancone", TonicOnBar);
        Leaf lemonSliceOnBar = new Leaf("Limone sul bancone", LemonSliceOnBar);

        Leaf doCocktail = new Leaf("Faccio il cocktail", DoCocktail);

        Leaf dayNight = new Leaf("Giorno/Notte", DayNight);

        LemonSliceReady.AddChild(reloadLemonSlice);
        LemonSliceReady.AddChild(lemonSliceOnBar);

        TonicReady.AddChild(reloadTonic);
        TonicReady.AddChild(tonicOnBar);

        LemonSodaReady.AddChild(reloadLemonSoda);
        LemonSodaReady.AddChild(lemonSodaOnBar);

        GinReady.AddChild(reloadGin);
        GinReady.AddChild(ginOnBar);

        VodkaReady.AddChild(reloadVodka);
        VodkaReady.AddChild(vodkaOnBar);

        cocktailDone.AddChild(VodkaReady);
        cocktailDone.AddChild(GinReady);
        cocktailDone.AddChild(LemonSodaReady);
        cocktailDone.AddChild(TonicReady);
        cocktailDone.AddChild(LemonSliceReady);
        cocktailDone.AddChild(doCocktail);
        cocktailDone.AddChild(dayNight);

        tree.AddChild(cocktailDone);

        tree.PrintTree();
    }

    private void Update()
    {
        
            treeStatus = tree.Process();
        currentVodka.text = vodkaStocked + "x";
        currentGin.text = ginStocked + "x";
        currentLemonSoda.text = lemonsodaStocked + "x";
        currentTonic.text = tonicStocked + "x";
        currentLemonSlice.text = lemonSliceStocked + "x";
    }
    BTNode.Status ReloadVodka()
    {
        return ReloadIngredients(ref vodkaStocked, spawner.enemies[0].GetComponent<Clienti>().requiredVodka, VodkaFridge);
    }

    BTNode.Status ReloadGin()
    {
        return ReloadIngredients(ref ginStocked, spawner.enemies[0].GetComponent<Clienti>().requiredGin, GinFridge);
    }

    BTNode.Status ReloadLemonSoda()
    {
        return ReloadIngredients(ref lemonsodaStocked, spawner.enemies[0].GetComponent<Clienti>().requiredLemonSoda, LemonSodaFridge);
    }

    BTNode.Status ReloadTonic()
    {
        return ReloadIngredients(ref tonicStocked, spawner.enemies[0].GetComponent<Clienti>().requiredTonic, TonicFridge);
    }

    BTNode.Status ReloadLemonSlice()
    {
        return ReloadIngredients(ref lemonSliceStocked, spawner.enemies[0].GetComponent<Clienti>().requiredLemonSlice, LemonSliceFridge);
    }

    BTNode.Status VodkaOnBar()
    {
        Debug.Log("vodka sul banco");
        return PutIngredientsOnBar(ref vodkaStocked, ref spawner.enemies[0].GetComponent<Clienti>().requiredVodka, vodkaPrefab);
    }
    BTNode.Status GinOnBar()
    {
        Debug.Log("gin sul banco");
        return PutIngredientsOnBar(ref ginStocked, ref spawner.enemies[0].GetComponent<Clienti>().requiredGin, ginPrefab);
    }
    BTNode.Status LemonSodaOnBar()
    {
        Debug.Log("soda sul banco");
        return PutIngredientsOnBar(ref lemonsodaStocked, ref spawner.enemies[0].GetComponent<Clienti>().requiredLemonSoda, lemonsodaPrefab);
    }
    BTNode.Status TonicOnBar()
    {
        Debug.Log("tonica sul banco");
        return PutIngredientsOnBar(ref tonicStocked, ref spawner.enemies[0].GetComponent<Clienti>().requiredTonic, tonicPrefab);
    }
    BTNode.Status LemonSliceOnBar()
    {
        Debug.Log("limone sul banco");
        return PutIngredientsOnBar(ref lemonSliceStocked, ref spawner.enemies[0].GetComponent<Clienti>().requiredLemonSlice, lemonslicePrefab);
    }
    BTNode.Status DayNight()
    {
        if (!TimeManager.instance.day && spawner.enemies.Count == 0)
        {
            transform.GetComponent<NavMeshAgent>().speed = 8;
            //transform.GetComponent<NavMeshAgent>().acceleration = 400;
            anim.SetBool("isWalking", true);
            agent.SetDestination(bed.position);
            if (Vector3.Distance(transform.position, bed.position) < 1f)
            {
                transform.rotation = bed.rotation;
                anim.SetBool("isWalking", false);
                anim.SetBool("isSleeping", true);
                return BTNode.Status.Running;
            }
            return BTNode.Status.Running;
        }
        else
        {
            return BTNode.Status.Success;
        }
    }

    BTNode.Status DoCocktail()
    {
        transform.GetComponent<NavMeshAgent>().speed = 4;
        //transform.GetComponent<NavMeshAgent>().acceleration = 8;
        anim.SetBool("isSleeping", false);
        anim.SetBool("isWalking", true);
        spotIndex = 0;
        agent.SetDestination(Bar.position);
        if (Vector3.Distance(transform.position, Bar.position) < 2)
        {
            transform.LookAt(Bar.position);
            anim.SetBool("isWalking", false);
            anim.SetBool("MakeOrder", true) ;
            Debug.Log("Do cocktail");
            timerToMixCocktail -= Time.deltaTime;
            if (timerToMixCocktail <= 0)
            {
                timerToMixCocktail = timeToMixCocktail;
                //spotIndex = 0;
                spawner.enemies[0].GetComponent<Clienti>().orderDone = true;
                for (int spotIndex = 0; spotIndex < ingredientsSpot.Length; spotIndex++)
                {
                    Destroy(ingredientsSpot[spotIndex].GetChild(0).gameObject);
                    anim.SetBool("MakeOrder", false) ;
                }
                

                return BTNode.Status.Success;
            }
            return BTNode.Status.Running;
        }else return BTNode.Status.Running;
    }

    BTNode.Status PutIngredientsOnBar(ref int ingredientsStocked, ref int ingredientsRequired, GameObject prefabToSpawn)
    {
        transform.GetComponent<NavMeshAgent>().speed = 4;
        //transform.GetComponent<NavMeshAgent>().acceleration = 8;
        anim.SetBool("isSleeping", false);
        if (ingredientsStocked >= ingredientsRequired && ingredientsRequired != 0)

        {
            anim.SetBool("isWalking", true);

            agent.SetDestination(Bar.position);

            if (Vector3.Distance(transform.position, Bar.position) < 2)
            {
                transform.LookAt(Bar.position);
                anim.SetBool("isWalking", false);
                timerToPutOnBar -= Time.deltaTime;
                if (timerToPutOnBar <= 0)
                {
                    timerToPutOnBar = timeToPutOnBar;

                    GameObject bottle = Instantiate(prefabToSpawn, ingredientsSpot[spotIndex].position, Quaternion.identity);
                    bottle.transform.parent = ingredientsSpot[spotIndex].transform;
                    if (spotIndex < 3)
                    {
                        spotIndex++;
                    }
                    else  if(spotIndex >= 3){
                        spotIndex = 0;
                    }

                    ingredientsStocked -= ingredientsRequired;
                    ingredientsRequired = 0;
                    Debug.Log("FIne");
                    return BTNode.Status.Success;
                }
            }
            return BTNode.Status.Running;
        }
        return BTNode.Status.Success;
    }

    BTNode.Status ReloadIngredients(ref int ingredientsStocked, int ingredientsToReload, Transform fridge)
    {
        transform.GetComponent<NavMeshAgent>().speed = 4;
        //transform.GetComponent<NavMeshAgent>().acceleration = 8;
        if (ingredientsStocked < ingredientsToReload)
        {
            anim.SetBool("isSleeping", false);
            agent.SetDestination(fridge.position);

            anim.SetBool("isWalking", true);
            if (Vector3.Distance(transform.position, fridge.position) < 2)
            {
                transform.LookAt(fridge.position);
                anim.SetBool("isWalking", false);
                timerToReload -= Time.deltaTime;
                if(timerToReload <= 0)
                {
                    ingredientsStocked = 5;
                    timerToReload = timeToReload;
                    return BTNode.Status.Success;
                }
                
            }
            return BTNode.Status.Running;
        }
        return BTNode.Status.Success;
    }
}
