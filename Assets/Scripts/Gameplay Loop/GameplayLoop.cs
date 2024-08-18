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
    [GetComponentInChildren][SerializeField] StoreClosing storeClosing;
    
    [Required][SerializeField] Store store;
    
    DaySummary lastSummary;
    bool running;

    [Foldout("Events")] public UnityEvent<DaySummary> onDayEnd;

    void Awake()
    {
        storeClosing.onCloseStore.AddListener(End);
        morning.onEndMorning.AddListener(OnMorningEnd);
        day.onDayEnd.AddListener(OnDayEnd);
    }
    void Start()
    {
        running = true;
        BeginMorning();
    }

    void End(CloseStoreSummary summary)
    {
        running = false;
    }
    void BeginMorning()
    {
        if (!running) return; 
        morning.BeginMorning();
    }
    void OnMorningEnd()
    {
        RunDay();
    }

    public void RunDay()
    {
        if (!running) return;
        lastSummary = daySummaryCalculator.CalculateSummary(store);
        day.RunDay(lastSummary);
    }

    void OnDayEnd()
    {
        EndDay();
    }

    void EndDay()
    {
        if (!running) return;
        store.EndDay(lastSummary);
        onDayEnd.Invoke(lastSummary);
        BeginMorning();
    }

    public void CloseStore()
    {
        storeClosing.CloseStore(store);
    }
}
