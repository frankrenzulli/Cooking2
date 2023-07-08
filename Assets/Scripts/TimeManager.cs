using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public bool day;

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Nessun time manager");
            return _instance;
        }
    }
}
