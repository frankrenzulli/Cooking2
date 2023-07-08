using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class BTConttroller : MonoBehaviour
{
    [SerializeField] private Transform VodkaFridge;
    [SerializeField] private Transform GinFridge;
    [SerializeField] private Transform LemonSodaFridge;
    [SerializeField] private Transform TonicFridge;
    [SerializeField] private Transform LemonSliceFridge;
    [SerializeField] private Transform Bar;

    [SerializeField] float timeToReload;
    private float timerToReload;
    [SerializeField] SpawnerClienti spawner;

    public enum ActionState { Idle, Working };
    ActionState state = ActionState.Idle;

    BTNode.Status treeStatus = BTNode.Status.Running;

    BTRoot tree;
    NavMeshAgent agent;

    private void Start()
    {
        timerToReload = timeToReload;
        agent = GetComponent<NavMeshAgent>();

        tree = new BTRoot();
        Sequence cocktailDone = new Sequence("CocktailFatto");
        Sequence VodkaReady = new Sequence("Vodka Sul Bancone");
        Sequence GinReady = new Sequence("Gin Sul Bancone");
        Sequence LemonSodaReady = new Sequence("Lemon Soda Sul Bancone");
        Sequence TonicReady = new Sequence("Tonica Sul Bancone");
        Sequence LemonSliceReady = new Sequence("Limone Sul Bancone");

        Leaf reloadVodka = new Leaf("Vodka ricaricata", ReloadVodka);
        Leaf reloadGin = new Leaf("Vodka ricaricata", ReloadGin);
        Leaf reloadLemonSoda = new Leaf("Vodka ricaricata", ReloadLemonSoda);
        Leaf reloadTonic = new Leaf("Vodka ricaricata", ReloadTonic);
        Leaf reloadLemonSlice = new Leaf("Vodka ricaricata", ReloadLemonSlice);

        Leaf vodkaOnBar = new Leaf("Vodka sul bancone", VodkaOnBar);
        Leaf ginOnBar = new Leaf("Vodka sul bancone", GinOnBar);
        Leaf lemonSodaOnBar = new Leaf("Vodka sul bancone", LemonSodaOnBar);
        Leaf tonicOnBar = new Leaf("Vodka sul bancone", TonicOnBar);
        Leaf lemonSliceOnBar = new Leaf("Vodka sul bancone", LemonSliceOnBar);

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
        LemonSliceReady.AddChild(lemonSliceOnBar);
        LemonSliceReady.AddChild(reloadLemonSlice);

        TonicReady.AddChild(tonicOnBar);
        TonicReady.AddChild(reloadTonic);

        LemonSodaReady.AddChild(lemonSodaOnBar);
        LemonSodaReady.AddChild(reloadLemonSoda);   
        
        GinReady.AddChild(ginOnBar);
        GinReady.AddChild(reloadGin);

        VodkaReady.AddChild(vodkaOnBar);
        VodkaReady.AddChild(reloadVodka);

        cocktailDone.AddChild(doCocktail);
        cocktailDone.AddChild(LemonSliceReady);
        cocktailDone.AddChild(TonicReady);
        cocktailDone.AddChild(LemonSodaReady);
        cocktailDone.AddChild(GinReady);
        cocktailDone.AddChild(VodkaReady);
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
        return BTNode.Status.Success;
    }

    BTNode.Status ReloadGin()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status ReloadLemonSoda()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status ReloadTonic()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status ReloadLemonSlice()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status VodkaOnBar()
    {
        return BTNode.Status.Success;
    }
    BTNode.Status GinOnBar()
    {
        return BTNode.Status.Success;
    }
    BTNode.Status LemonSodaOnBar()
    {
        return BTNode.Status.Success;
    }
    BTNode.Status TonicOnBar()
    {
        return BTNode.Status.Success;
    }
    BTNode.Status LemonSliceOnBar()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status DoCocktail()
    {
        return BTNode.Status.Success;
    }

    BTNode.Status PutIngredientsOnBar()
    {
        agent.SetDestination(Bar.position);
        if (Vector3.Distance(transform.position, Bar.position) <= 1) 
        {

        }
        return BTNode.Status.Running;
    }

    BTNode.Status ReloadIngredients(ref int ingredientsStocked, int ingredientsToReload, Transform fridge)
    {
        if (ingredientsStocked <= ingredientsToReload)
        {
            agent.SetDestination(fridge.position);
            if (Vector3.Distance(transform.position, fridge.position) < 1)
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
