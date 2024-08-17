using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ClientsCap : MonoBehaviour
{
    [SerializeField] int capPerPair;
    
    [ReadOnly][SerializeField] int clientsCap;

    public int Recalculate(List<IStoreObject> storeObjects)
    {
        clientsCap = Calculate(storeObjects);
        return clientsCap;
    }
    int Calculate(List<IStoreObject> storeObjects)
    {
        var cashes = 0;
        var cashiers = 0;
        foreach (var storeObject in storeObjects){
            var shopObjectType = storeObject.GetShopObjectType();
            cashes += shopObjectType.clientsCapImpact.cashValue;
            cashiers += shopObjectType.clientsCapImpact.cashierValue;
        }
        var pairs = Mathf.Min(cashes, cashiers);
        var value = pairs * capPerPair; 
        return value;
    }
}