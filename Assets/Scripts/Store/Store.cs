using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] Workers workers;
    [GetComponentInChildren][SerializeField] PaidActions paidAction;
    [GetComponentInChildren][SerializeField] Money money;
    [GetComponentInChildren][SerializeField] Dirt dirt;
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
    public Money GetMoney()
    {
        return money;
    }

    public void EndDay(DaySummary summary)
    {
        money.ModifyValue(summary.income);
        dirt.ModifyValue(summary.dirtGenerated);
        workers.DayPassed();
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

    public Dirt GetDirt()
    {
        return dirt;
    }

    public PaidActions GetPaidActions()
    {
        return paidAction;
    }
}