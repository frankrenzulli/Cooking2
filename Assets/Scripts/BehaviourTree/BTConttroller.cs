using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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



    [SerializeField] private int spotIndex = 0;



    public enum ActionState { Idle, Working };
    ActionState state = ActionState.Idle;

    BTNode.Status treeStatus = BTNode.Status.Running;

    BTRoot tree;
    NavMeshAgent agent;

    private void Start()
    {
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

        //Leaf goToItem = new Leaf("Raggiungi l'Item", GoToItem);
        //Leaf getToSafety = new Leaf("Scappa dall'edificio", GetToSafety);
        //Selector openDoor = new Selector("Open Door");

        //openDoor.AddChild(goToFrontDoor);
        //openDoor.AddChild(goToBackDoor);

        //theft.AddChild(openDoor);
        //theft.AddChild(goToItem);
        //theft.AddChild(getToSafety);
        //tree.AddChild(theft);


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

        tree.AddChild(cocktailDone);

        tree.PrintTree();
        //tree.PrintTree();

    }

    private void Update()
    {
        
            treeStatus = tree.Process();
    }

    /*public BTNode.Status GoToDoor(Transform door)
    {
        BTNode.Status s = GoToLocation(door.position);

        if (s == BTNode.Status.Success)
        {
            if (!door.GetComponent<Lock>().isLocked)
            {
                door.gameObject.SetActive(false);
                return BTNode.Status.Success;
            }
            return BTNode.Status.Failure;
        }

        return s;
    }

    public BTNode.Status GoToFrontDoor()
    {
        return GoToDoor(frontDoor);
    }

    public BTNode.Status GoToBackDoor()
    {
        return GoToDoor(backDoor);
    }

    public BTNode.Status GoToItem()
    {
        return GoToLocation(treasure.position);
    }

    public BTNode.Status GetToSafety()
    {
        return GoToLocation(safeZone.position);
    }

    

    BTNode.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, transform.position);

        if (state == ActionState.Idle)
        {
            agent.SetDestination(destination);
            state = ActionState.Working;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.Idle;
            Debug.Log("Destinazione non raggiungibile");
            return BTNode.Status.Failure;
        }
        else if (distanceToTarget < 2)
        {
            state = ActionState.Idle;
            return BTNode.Status.Success;
        }

        return BTNode.Status.Running;
    }*/
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

    BTNode.Status DoCocktail()
    {
        spotIndex = 0;
        agent.SetDestination(Bar.position);
        if (Vector3.Distance(transform.position, Bar.position) < 2)
        {
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
                }
                

                return BTNode.Status.Success;
            }
            return BTNode.Status.Running;
        }else return BTNode.Status.Running;
    }

    /*BTNode.Status PutIngredientsOnBar(ref int ingredientsStocked, ref int ingredientsRequired, GameObject prefabToSpawn)
    {
        if (ingredientsRequired <= ingredientsStocked)
        {
            Debug.Log("Debug 1");
            agent.SetDestination(Bar.position);
            if (Vector3.Distance(transform.position, Bar.position) <= 2)
            {

                Debug.Log("Debug 2");
                timerToPutOnBar -= Time.deltaTime;
                if (timerToPutOnBar <= 0)
                {
                    Debug.Log("Debug 3");
                    timerToPutOnBar = timeToPutOnBar;
                    GameObject bottle = Instantiate(prefabToSpawn, ingredientsSpot[spotIndex].position, Quaternion.identity);
                    ingredientsRequired = 0;
                    ingredientsStocked -= ingredientsRequired;
                    //ingredientsRequired = 0;
                    if (spotIndex >= ingredientsSpot.Length)
                    {
                        spotIndex = 0;
                    }
                    else spotIndex++;
                    

                    return BTNode.Status.Success;
                }
            }
            return BTNode.Status.Running;
        }
         return BTNode.Status.Success;
    }*/

    BTNode.Status PutIngredientsOnBar(ref int ingredientsStocked, ref int ingredientsRequired, GameObject prefabToSpawn)
    {
        if(ingredientsStocked >= ingredientsRequired && ingredientsRequired != 0)
        {
            agent.SetDestination(Bar.position);
            if (Vector3.Distance(transform.position, Bar.position) < 2)
            {
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
        if (ingredientsStocked < ingredientsToReload)
        {
            agent.SetDestination(fridge.position);
            if (Vector3.Distance(transform.position, fridge.position) < 2)
            {
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
