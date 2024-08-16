using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TestMetrics : MonoBehaviour
{
    [Required][SerializeField] StoreAttractiveness storeAttractiveness;
    [Required][SerializeField] ClientsCap clientsCap;
    [Required][SerializeField] ClientsPerDay clientsPerDay;
    [Required][SerializeField] Store store;

    [Button]
    void TestClientsPerDay()
    {
        List<IStoreObject> shopObjects = store.GetStoreObjects();
        var attractiveness = storeAttractiveness.Recalculate(shopObjects);
        var clientsCapValue = clientsCap.Recalculate(shopObjects);
        var clients = clientsPerDay.Recalculate(attractiveness, clientsCapValue);
        Debug.Log(clients);
    }
}
