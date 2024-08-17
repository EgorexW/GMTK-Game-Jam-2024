using System;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourRunner : MonoBehaviour, IBehaviourRunner
{
    public float timeMod = 1;
    [SerializeField] List<GameBehaviour> behaviours = new();

    void Awake()
    {
        AddChildren();
    }

    void AddChildren()
    {
        foreach (var behaviour in GetComponentsInChildren<GameBehaviour>()){
            AddBehaviour(behaviour);
        }
    }

    public void RemoveBehaviour(GameBehaviour behaviour)
    {
        behaviours.Remove(behaviour);
    }
    public void AddBehaviour(GameBehaviour behaviour)
    {
        if (behaviours.Contains(behaviour)){
            return;
        }
        behaviours.Add(behaviour);
        behaviour.onDestroy.AddListener(RemoveBehaviour);
    }

    public void Run()
    {
        AddChildren();
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