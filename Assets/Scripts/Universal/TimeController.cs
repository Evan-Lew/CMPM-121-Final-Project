using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine.ProBuilder;

public class TimeController : MonoBehaviour
{

    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;



    private DateTime currentTime;





    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);



    }

    private void FixedUpdate()
    {
        updateTimeofDay();



    }


    void updateTimeofDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

}


