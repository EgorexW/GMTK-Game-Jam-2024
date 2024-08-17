using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

public class DaySummaryCalculator : MonoBehaviour
{
    [GetComponentInChildren][SerializeField] StoreAttractiveness storeAttractiveness;
    [GetComponentInChildren][SerializeField] ClientsCap clientsCap;
    [GetComponentInChildren][SerializeField] ClientsPerDay clientsPerDay;
    [GetComponentInChildren][SerializeField] RevenuePerClient revenuePerClient;
    [GetComponentInChildren][SerializeField] StoreCleanness storeCleanness;
    [GetComponentInChildren][SerializeField] Revenue revenue;
    [GetComponentInChildren][SerializeField] Costs costs;

    public DaySummary CalculateSummary(Store store)
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        Workers workers = store.GetWorkers();
        var storeCleannessValue = storeCleanness.Recalculate(shopObjects);
        var attractiveness = storeAttractiveness.Recalculate(shopObjects, storeCleannessValue);
        var clientsCapValue = clientsCap.Recalculate(shopObjects);
        var clients = clientsPerDay.Recalculate(attractiveness, clientsCapValue); 
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects); 
        var revenueValue = revenue.Recalculate(revenuePerClientValue, clients);
        var costsValue = costs.Recalculate(workers);
        var income = revenueValue - costsValue;
        var summary = new DaySummary{
            income = income,
            clients = clients
        };
        return summary;
    }
}

public struct DaySummary
{
    public float income;
    public float clients;
}