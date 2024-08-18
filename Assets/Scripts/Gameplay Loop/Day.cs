using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Day : MonoBehaviour
{
    [Required][SerializeField] DayUI dayUI;
    [SerializeField] float dayTime = 30;

    [SerializeField] float baseTimeMod = 2;
    [ReadOnly][SerializeField] float timeMod = 1;

    [Foldout("Events")] public UnityEvent onDayEnd = new();

    float dayTimeElapsed;
    bool dayRunning;
    StoreRunner storeRunner;

    void Awake()
    {
        ChangeTimeMod(1);
    }

    void Start()
    {
        dayUI.Hide();
    }

    public void RunDay(Store store, DaySummary lastSummary)
    {
        storeRunner = store.GetStoreRunner();
        storeRunner.RunStore(lastSummary);
        storeRunner.ChangeTimeMod(timeMod);
        dayUI.Show();
        dayRunning = true;
        dayTimeElapsed = 0;
    }

    void Update()
    {
        if (!dayRunning){
            return;
        }
        dayTimeElapsed += GetDeltaTime();
        if (dayTimeElapsed >= dayTime){
            EndDay();
        }
    }

    void EndDay()
    {
        storeRunner.EndRun();
        dayUI.Hide();
        dayRunning = false;
        onDayEnd.Invoke();
    }

    float GetDeltaTime()
    {
        return Time.deltaTime * timeMod;
    }

    public void ChangeTimeMod(float newValue)
    {
        timeMod = newValue * baseTimeMod;
        if (storeRunner != null) storeRunner.ChangeTimeMod(timeMod);
    }
}
