using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class StoreCleanness : MonoBehaviour
{
    [SerializeField] float baseCleanningValue = 5f;
    
    [ReadOnly][SerializeField] float storeCleanness;

    [Foldout("Events")] public UnityEvent<float> onRecalculate;

    public float Recalculate(List<IStoreObject> storeObjects)
    {
        storeCleanness = Calculate(storeObjects);
        onRecalculate.Invoke(storeCleanness);
        return storeCleanness;
    }

    float Calculate(List<IStoreObject> storeObjects)
    {
        var dirtinessArea = 0f;
        var cleanningValue = baseCleanningValue;
        foreach (var storeObject in storeObjects){
            var shopObjectType = storeObject.GetShopObjectType();
            dirtinessArea += shopObjectType.cleannessImpact.dirtinessArea;
            cleanningValue += shopObjectType.cleannessImpact.cleaningValue;
        }
        var value = cleanningValue / dirtinessArea;
        value = Mathf.Clamp01(value);
        return value;
    }
}