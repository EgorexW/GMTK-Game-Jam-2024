using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourRunner : MonoBehaviour, IBehaviourRunner
{
    public float timeMod = 1;
    [SerializeField] List<GameBehaviour> behaviours = new();

    void Awake()
    {
        foreach (var behaviour in GetComponentsInChildren<GameBehaviour>()){
            if (behaviours.Contains(behaviour)){
                return;
            }
            behaviours.Add(behaviour);
        }
    }

    public void Run()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.Run(this);
        }
    }

    public void End()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.End();
        }
        
    }
    public float GetDeltaTime()
    {
        return Time.deltaTime * timeMod;
    }
}