using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ClientsCap : MonoBehaviour
{
    [SerializeField] int capMod = 1;
    
    [ReadOnly][SerializeField] int clientsCap;

    public int Recalculate(List<IStoreObject> storeObjects)
    {
        clientsCap = Calculate(storeObjects);
        return clientsCap;
    }
    int Calculate(List<IStoreObject> storeObjects)
    {
        var cashingValue = 0;
        foreach (var storeObject in storeObjects){
            var shopObjectType = storeObject.GetStoreObjectType();
            cashingValue += shopObjectType.clientsCapImpact.cashingValue;
        }
        var value = cashingValue * capMod; 
        return value;
    }
}