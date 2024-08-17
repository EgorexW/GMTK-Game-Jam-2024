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
    [FormerlySerializedAs("dayEnd")] [GetComponentInChildren][SerializeField] DaySummaryCalculator daySummaryCalculator;
    
    [Required][SerializeField] Store store;

    void Awake()
    {
        day.onDayEnd.AddListener(OnDayEnd);
    }
    [Button]
    public void RunDay()
    {
        day.RunDay();
    }

    void OnDayEnd()
    {
        var summary = daySummaryCalculator.EndDay(store);
    }

}
