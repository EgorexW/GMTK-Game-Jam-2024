using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class StoreAttractiveness : MonoBehaviour
{
    [ReadOnly][SerializeField] float attractiveness;

    public float Recalculate(List<IStoreObject> shopObjects)
    {
        attractiveness = Calculate(shopObjects);
        return attractiveness;
    }
    float Calculate(List<IStoreObject> shopObjects)
    {
        var value = 0f;
        var valueCap = 0f;
        foreach (var shopObject in shopObjects){
            var attractivenessImpact = shopObject.GetShopObjectType().attractivenessImpact;
            value += attractivenessImpact.baseValue;
            valueCap += attractivenessImpact.capValue;
        }
        var cappedValue = math.min(value, valueCap);
        return cappedValue;
    }
}