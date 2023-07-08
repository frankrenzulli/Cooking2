using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public bool day;

    private void Start()
    {
        instance = this;
    }
}
