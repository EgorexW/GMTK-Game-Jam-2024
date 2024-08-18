using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class StoreAttractiveness : MonoBehaviour
{
    [SerializeField] float baseAttractiveness = 10;
    
    [ReadOnly][SerializeField] float attractiveness;

    public float Recalculate(List<IStoreObject> shopObjects, float storeCleanness)
    {
        attractiveness = Calculate(shopObjects, storeCleanness);
        return attractiveness;
    }
    float Calculate(List<IStoreObject> shopObjects, float storeCleanness)
    {
        var value = baseAttractiveness;
        foreach (var shopObject in shopObjects){
            var attractivenessImpact = shopObject.GetShopObjectType().attractivenessImpact;
            value += attractivenessImpact.baseValue;
        }
        return value * storeCleanness;
    }
}