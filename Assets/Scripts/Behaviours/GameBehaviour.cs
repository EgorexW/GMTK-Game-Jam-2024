using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    protected IBehaviourRunner runner;

    public virtual void Run(IBehaviourRunner runnerTmp)
    {
        this.runner = runnerTmp;
    }

    protected float GetDeltaTime()
    {
        return runner?.GetDeltaTime() ?? Time.deltaTime;
    }
    public virtual void End()
    {
        runner = null;
    }
}

public interface IBehaviourRunner
{
    float GetDeltaTime();
}