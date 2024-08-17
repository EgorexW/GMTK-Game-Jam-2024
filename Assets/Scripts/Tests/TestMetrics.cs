using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class TestMetrics : MonoBehaviour
{
    [Required][SerializeField] Workers workers;
    [Required][SerializeField] StoreAttractiveness storeAttractiveness;
    [Required][SerializeField] ClientsCap clientsCap;
    [Required][SerializeField] ClientsPerDay clientsPerDay;
    [Required][SerializeField] RevenuePerClient revenuePerClient;
    [Required][SerializeField] StoreCleanness storeCleanness;
    [FormerlySerializedAs("revenueCalculator")] [Required][SerializeField] Revenue revenue;
    [Required][SerializeField] Store store;
    [FormerlySerializedAs("costsCalculator")] [Required][SerializeField] Costs costs;

    [Button]
    void GetIncome()
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        var storeCleannessValue = storeCleanness.Recalculate(shopObjects);
        var attractiveness = storeAttractiveness.Recalculate(shopObjects, storeCleannessValue);
        var clientsCapValue = clientsCap.Recalculate(shopObjects);
        var clients = clientsPerDay.Recalculate(attractiveness, clientsCapValue); 
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects); 
        var revenueValue = revenue.Recalculate(revenuePerClientValue, clients);
        var costsValue = costs.Recalculate(workers);
        var income = revenueValue - costsValue;
        Debug.Log($"Income: {income}");
    }
}