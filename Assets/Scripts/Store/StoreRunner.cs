using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class StoreRunner : MonoBehaviour
{    
    [GetComponentInChildren][SerializeField] BehaviourRunner behaviourRunner;
    [GetComponentInChildren] [SerializeField] ObjectsFactory customersSpawner;
    public void RunStore(DaySummary lastSummary)
    {
        customersSpawner.Clear();
        customersSpawner.SetCount(lastSummary.totalCustomers);
        behaviourRunner.Run();
    }

    public void ChangeTimeMod(float timeMod)
    {
        behaviourRunner.timeMod = timeMod;
    }

    public void EndRun()
    {
        behaviourRunner.End();
    }
}