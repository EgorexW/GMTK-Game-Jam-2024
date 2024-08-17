using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RevenuePerClient : MonoBehaviour
{
    [SerializeField] float baseRevenuePerClient;
    [ReadOnly][SerializeField] float revenuePerClient;

    public float Recalculate(List<IStoreObject> shopObjects)
    {
        revenuePerClient = Calculate(shopObjects);
        return revenuePerClient;
    }

    float Calculate(List<IStoreObject> shopObjects)
    {
        var value = baseRevenuePerClient;
        foreach (var shopObject in shopObjects){
            var revenueImpact = shopObject.GetShopObjectType().revenueImpact;
            value += revenueImpact.baseValue;
        }
        return value;
    }
}