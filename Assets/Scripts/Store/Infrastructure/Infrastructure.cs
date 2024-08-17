using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Infrastructure : MonoBehaviour
{
    [Required] [SerializeField] Money money;
    
    Dictionary<InfrastructureType, List<InfrastructureObject>> infrastructureByType = new();

    [Foldout("Events")] public UnityEvent<InfrastructureObject> onRemoveInfrastructure;
    
    void Awake()
    {
        foreach (var infrastructure in GetComponentsInChildren<InfrastructureObject>()){
            AddInfrastructure(infrastructure);
        }
    }

    void AddInfrastructure(InfrastructureObject infrastructure)
    {
        if (!infrastructureByType.TryGetValue(infrastructure.GetInfrastructureType(), out var infrastructureOfType)){
            infrastructureOfType = new List<InfrastructureObject>();
            infrastructureByType[infrastructure.GetInfrastructureType()] = infrastructureOfType;
        }
        infrastructureOfType.Add(infrastructure);
    }

    public List<InfrastructureObject> GetInfrastructureOfType(InfrastructureType infrastructureType)
    {
        return infrastructureByType[infrastructureType];
    }

    public void Sell(InfrastructureObject infrastructureObject)
    {
        var sellValue = infrastructureObject.GetInfrastructureType().sellValue;
        money.ModifyValue(sellValue);
        infrastructureObject.Remove();
        RemoveInfrastructure(infrastructureObject);
    }

    void RemoveInfrastructure(InfrastructureObject infrastructureObject)
    {
        infrastructureByType[infrastructureObject.GetInfrastructureType()].Remove(infrastructureObject);
        onRemoveInfrastructure.Invoke(infrastructureObject);
    }
}