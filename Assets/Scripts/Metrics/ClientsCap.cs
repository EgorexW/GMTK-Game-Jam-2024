using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ClientsCap : MonoBehaviour
{
    [SerializeField] float capPerPair;
    
    [ReadOnly][SerializeField] float clientsCap;

    public float Recalculate(List<IStoreObject> storeObjects)
    {
        clientsCap = Calculate(storeObjects);
        return clientsCap;
    }
    float Calculate(List<IStoreObject> storeObjects)
    {
        var cashes = 0f;
        var cashiers = 0f;
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