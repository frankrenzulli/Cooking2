using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class BTConttroller : MonoBehaviour
{
    [SerializeField] private Transform treasure;
    [SerializeField] private Transform safeZone;
    [SerializeField] private Transform frontDoor;
    [SerializeField] private Transform backDoor;

    public enum ActionState { Idle, Working };
    ActionState state = ActionState.Idle;

    BTNode.Status treeStatus = BTNode.Status.Running;

    BTRoot tree;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BTRoot();
        Sequence theft = new Sequence("Recupera il tesoro");
        //Leaf goToFrontDoor = new Leaf("Raggiungi la porta principale", GoToFrontDoor);
        //Leaf goToBackDoor = new Leaf("Raggiungi la porta sul retro", GoToBackDoor);
        //Leaf goToItem = new Leaf("Raggiungi l'Item", GoToItem);
        //Leaf getToSafety = new Leaf("Scappa dall'edificio", GetToSafety);
        //Selector openDoor = new Selector("Open Door");

        //openDoor.AddChild(goToFrontDoor);
        //openDoor.AddChild(goToBackDoor);

        //theft.AddChild(openDoor);
        //theft.AddChild(goToItem);
        //theft.AddChild(getToSafety);
        //tree.AddChild(theft);

        //tree.PrintTree();

    }

    private void Update()
    {
        if (treeStatus == BTNode.Status.Running)
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
}
