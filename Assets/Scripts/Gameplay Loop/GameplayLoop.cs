using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayLoop : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Day day;
    [GetComponentInChildren][SerializeField] DaySummaryCalculator daySummaryCalculator;
    [GetComponentInChildren][SerializeField] Morning morning;
    
    [Required][SerializeField] Store store;

    void Awake()
    {
        morning.onEndMorning.AddListener(OnMorningEnd);
        day.onDayEnd.AddListener(OnDayEnd);
    }
    void Start()
    {
        BeginMorning();
    }
    void BeginMorning()
    {
        morning.BeginMorning();
    }
    void OnMorningEnd()
    {
        RunDay();
    }

    public void RunDay()
    {
        day.RunDay();
    }

    void OnDayEnd()
    {
        EndDay();
    }

    void EndDay()
    {
        var summary = daySummaryCalculator.CalculateSummary(store);
        store.EndDay(summary);
        BeginMorning();
    }

}
