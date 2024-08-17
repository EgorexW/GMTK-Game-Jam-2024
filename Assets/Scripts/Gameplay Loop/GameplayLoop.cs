using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameplayLoop : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Day day;
    [GetComponentInChildren][SerializeField] DaySummaryCalculator daySummaryCalculator;
    [GetComponentInChildren][SerializeField] Morning morning;
    
    [Required][SerializeField] Store store;
    
    DaySummary lastSummary;

    [Foldout("Events")] public UnityEvent<DaySummary> onDayEnd;

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
        lastSummary = daySummaryCalculator.CalculateSummary(store);
        day.RunDay(lastSummary);
    }

    void OnDayEnd()
    {
        EndDay();
    }

    void EndDay()
    {
        store.EndDay(lastSummary);
        onDayEnd.Invoke(lastSummary);
        BeginMorning();
    }

}
