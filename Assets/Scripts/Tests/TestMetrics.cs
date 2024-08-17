using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TestMetrics : MonoBehaviour
{
    [Required][SerializeField] StoreAttractiveness storeAttractiveness;
    [Required][SerializeField] ClientsCap clientsCap;
    [Required][SerializeField] ClientsPerDay clientsPerDay;
    [Required][SerializeField] RevenuePerClient revenuePerClient;
    [Required][SerializeField] Revenue revenue;
    [Required][SerializeField] Store store;

    [Button]
    void GetRevenue()
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        var attractiveness = storeAttractiveness.Recalculate(shopObjects);
        var clientsCapValue = clientsCap.Recalculate(shopObjects);
        var clients = clientsPerDay.Recalculate(attractiveness, clientsCapValue); 
        var revenuePerClientValue = revenuePerClient.Recalculate(shopObjects); 
        var revenueValue = revenue.Recalculate(revenuePerClientValue, clients);
        Debug.Log($"Revenue: {revenueValue}");
    }
}