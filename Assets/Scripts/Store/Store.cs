using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Workers workers;
    [GetComponentInChildren][SerializeField] Money money;
    [GetComponentInChildren][SerializeField] Infrastructure infrastructure;
    [GetComponentInChildren][SerializeField] DaySummaryCalculator daySummaryCalculator;
    [GetComponentInChildren][SerializeField] StoreClosing storeClosing;
    [GetComponentInChildren][SerializeField] StoreRunner storeRunner;
    
    [Foldout("Events")] public UnityEvent<CloseStoreSummary> onCloseStore;

    public DaySummaryCalculator GetDaySummaryCalculator()
    {
        return daySummaryCalculator;
    }

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
        infrastructure.DayPassed();
        daySummaryCalculator.DayPassed();
        storeClosing.DayPassed();
    }

    public void CloseStore()
    {
        var summary = storeClosing.CloseStore(this);
        onCloseStore.Invoke(summary);
    }

    public StoreRunner GetStoreRunner()
    {
        return storeRunner;
    }
}