using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    [Header("Directional Light Rotations & Reference")]
    [SerializeField] private Quaternion dayRotation = Quaternion.Euler(new Vector3(50f, -30f, 0f));
    [SerializeField] private Quaternion nightRotation = Quaternion.Euler(new Vector3(170f, -30f, 0f));
    public Light directionalLight;

    [Header("Time Variables")]
    [SerializeField] private float timer = 0f;
    public float dayDuration = 30f;
    public float nightDuration = 10f;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI dayNight;
    [SerializeField] Color dayColor;
    [SerializeField] Color nightColor;

    [Header("Bools")]
    public bool day;
    private bool isDayTimeFinished = false;


    private void Start()
    {
        instance = this;
        directionalLight.transform.rotation = dayRotation;
        day = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Time.timeScale = 1;
        }
        timer += Time.deltaTime;

        if (day)
        {
            dayNight.text = "DAY";
            dayNight.color = dayColor;
            if (timer >= dayDuration)
            {
                day = false;
                timer = 0f;
                isDayTimeFinished = true;
            }
            else
            {
                float angle = Mathf.Lerp(0f, 180f, timer / dayDuration);
                directionalLight.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
            }
        }
        else
        {
            dayNight.text = "NIGHT";
            dayNight.color = nightColor;
            if (isDayTimeFinished)
            {
                directionalLight.transform.rotation = Quaternion.Euler(0f, -180f, -180f);
                isDayTimeFinished = false;
            }

            if (timer >= nightDuration)
            {
                day = true;
                timer = 0f;
            }
        }
    }
}
