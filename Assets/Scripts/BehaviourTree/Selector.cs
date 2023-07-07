using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BTNode
{

    public Selector(string n)
    {
        name = n;
    }

    // a differenza di sequence, questo va visto come un ||, ovvero che ha successo se anche almeno uno dei processi ha successo
    public override Status Process()
    {
        Status childStatus = children[currentChild].Process();


        //childstatus == running ==> running
        if (childStatus == Status.Running) return Status.Running;

        //childstatus == running ==> success
        if (childStatus == Status.Success)
        {
            currentChild = 0;
            return Status.Success;
        }


        //childstatus == failure ==> running + prossimo, se sono all'ultimo child ==> failure
        currentChild++;
        if(currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.Failure;
        }
        return Status.Running;
        
        
        
    }

}
