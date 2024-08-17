using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    protected IBehaviourRunner runner;
    protected bool running;

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
}

public interface IBehaviourRunner
{
    float GetDeltaTime();
}