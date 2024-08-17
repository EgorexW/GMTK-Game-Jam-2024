using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class Store : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Workers workers;
    [GetComponentInChildren][SerializeField] Money money;
    [GetComponentInChildren][SerializeField] Infrastructure infrastructure;

    public List<IStoreObject> GetStoreObjects()
    {
        return new List<IStoreObject>(GetComponentsInChildren<IStoreObject>());
    }

    public Workers GetWorkers()
    {
        return workers;
    }
    public Infrastructure GetInfrastructure()
    {
        return infrastructure;
    }
    public Money GetMoney()
    {
        return money;
    }

    public void EndDay(DaySummary summary)
    {
        money.ModifyValue(summary.income);
        workers.DayPassed();
    }
}