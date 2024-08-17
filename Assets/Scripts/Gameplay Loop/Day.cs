using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Day : MonoBehaviour
{
    [Required][SerializeField] BehaviourRunner behaviourRunner;
    [SerializeField] float dayTime = 30;
    
    [SerializeField] float timeMod = 1;

    [Foldout("Events")] public UnityEvent onDayEnd = new();

    float dayTimeElapsed;
    bool dayRunning;

    public void RunDay()
    {
        behaviourRunner.Run();
        dayRunning = true;
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
        timeMod = newValue;
        behaviourRunner.timeMod = newValue;
    }
}
