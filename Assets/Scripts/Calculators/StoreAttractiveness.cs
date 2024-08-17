using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class StoreAttractiveness : MonoBehaviour
{
    [ReadOnly][SerializeField] float attractiveness;

    public float Recalculate(List<IStoreObject> shopObjects, float storeCleanness)
    {
        attractiveness = Calculate(shopObjects, storeCleanness);
        return attractiveness;
    }
    float Calculate(List<IStoreObject> shopObjects, float storeCleanness)
    {
        var value = 0f;
        foreach (var shopObject in shopObjects){
            var attractivenessImpact = shopObject.GetShopObjectType().attractivenessImpact;
            value += attractivenessImpact.baseValue;
        }
        return value * storeCleanness;
    }
}