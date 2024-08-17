using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Day : MonoBehaviour
{
    [Required][SerializeField] DayUI dayUI;
    [Required][SerializeField] BehaviourRunner behaviourRunner;
    [Required] [SerializeField] ObjectsFactory customersSpawner;
    [SerializeField] float dayTime = 30;

    [SerializeField] float baseTimeMod = 2;
    [ReadOnly][SerializeField] float timeMod = 1;

    [Foldout("Events")] public UnityEvent onDayEnd = new();

    float dayTimeElapsed;
    bool dayRunning;

    void Awake()
    {
        ChangeTimeMod(1);
    }

    void Start()
    {
        dayUI.Hide();
    }

    public void RunDay(DaySummary lastSummary)
    {
        customersSpawner.Clear();
        customersSpawner.SetCount(lastSummary.totalCustomers);
        dayUI.Show();
        behaviourRunner.Run();
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
        dayUI.Hide();
        behaviourRunner.End();
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
        behaviourRunner.timeMod = timeMod;
    }
}
