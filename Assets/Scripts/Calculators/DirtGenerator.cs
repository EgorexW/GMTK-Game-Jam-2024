using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DirtGenerator : MonoBehaviour
{
    [SerializeField] float dirtPerCustomer;
    
    [ReadOnly][SerializeField] int dirtGenerated;

    [Foldout("Events")] public UnityEvent<float> onRecalculate;

    public float Recalculate(List<IStoreObject> storeObjects, int customersNr)
    {
        dirtGenerated = Calculate(storeObjects, customersNr);
        onRecalculate.Invoke(dirtGenerated);
        return dirtGenerated;
    }

    int Calculate(List<IStoreObject> storeObjects, int customersNr)
    {
        var value = customersNr * dirtPerCustomer;
        foreach (var storeObject in storeObjects){
            var shopObjectType = storeObject.GetStoreObjectType();
            value -= shopObjectType.cleannessImpact.dirtGeneration;
        }
        return Mathf.RoundToInt(value);
    }
}