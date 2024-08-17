using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameBehaviour : MonoBehaviour
{
    protected IBehaviourRunner runner;
    protected bool running;

    [Foldout("Events")] public UnityEvent<GameBehaviour> onDestroy;

    public virtual void Run(IBehaviourRunner runnerTmp)
    {
        runner = runnerTmp;
        running = true;
    }

    protected float GetDeltaTime()
    {
        return runner?.GetDeltaTime() ?? Time.deltaTime;
    }
    public virtual void End()
    {
        running = false;
    }

    protected void OnDestroy()
    {
        onDestroy.Invoke(this);
    }
}

public interface IBehaviourRunner
{
    float GetDeltaTime();
}