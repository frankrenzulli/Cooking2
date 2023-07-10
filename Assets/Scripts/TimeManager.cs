using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public bool day;

    public Light directionalLight;
    public float dayDuration = 30f;
    public float nightDuration = 10f;

    [SerializeField] private Quaternion dayRotation = Quaternion.Euler(new Vector3(50f, -30f, 0f));
    [SerializeField] private Quaternion nightRotation = Quaternion.Euler(new Vector3(170f, -30f, 0f));

    [SerializeField] private float timer = 0f;

    private void Start()
    {
        instance = this;
        directionalLight.transform.rotation = dayRotation;
        day = true;
    }
    private void Update()
    {
        // Aggiorna il timer
        timer += Time.deltaTime;

        if (day)
        {
            // � giorno
            if (timer >= dayDuration)
            {
                // Passa alla notte
                day = false;
                timer = 0f;
            }
        }
        else
        {
            // � notte
            if (timer >= nightDuration)
            {
                // Passa al giorno
                day = true;
                timer = 0f;
            }
        }

        // Ruota la directional light per rappresentare il cambio tra giorno e notte
        float angle = Mathf.Lerp(0f, 360f, timer / (day ? dayDuration : nightDuration));
        directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);

        // Aggiorna l'intensit� della luce in base al periodo del giorno
        float intensity = day ? 1f : 0f;
        directionalLight.intensity = intensity;
    }
}
