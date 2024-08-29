using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class DirtGenerated : MonoBehaviour
{
    [SerializeField] float dirtPerCustomer;
    
    [ReadOnly][SerializeField] float storeCleanness;

    [Foldout("Events")] public UnityEvent<float> onRecalculate;

    public float Recalculate(List<IStoreObject> storeObjects, int customersNr)
    {
        storeCleanness = Calculate(storeObjects, customersNr);
        onRecalculate.Invoke(storeCleanness);
        return storeCleanness;
    }

    float Calculate(List<IStoreObject> storeObjects, int customersNr)
    {
        var dirtGenerated = customersNr * dirtPerCustomer;
        foreach (var storeObject in storeObjects){
            var shopObjectType = storeObject.GetStoreObjectType();
            dirtGenerated -= shopObjectType.cleannessImpact.dirtGeneration;
        }
        return dirtGenerated;
    }
}