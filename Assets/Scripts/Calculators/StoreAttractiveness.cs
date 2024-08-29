using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StoreAttractiveness : MonoBehaviour
{
    [SerializeField] float baseAttractiveness = 10;
    
    [ReadOnly][SerializeField] float attractiveness;

    public float Recalculate(List<IStoreObject> shopObjects)
    {
        attractiveness = Calculate(shopObjects);
        return attractiveness;
    }
    float Calculate(List<IStoreObject> shopObjects)
    {
        var value = baseAttractiveness;
        foreach (var shopObject in shopObjects){
            var attractivenessImpact = shopObject.GetStoreObjectType().attractivenessImpact;
            value += attractivenessImpact.baseValue;
        }
        return value;
    }
}